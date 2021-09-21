using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Authorization;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReviewInputValueController : Controller
    {
        private IGisCountryValueRepository _gcValueRepository;
        private IGisAddonValueRepository _addonValueRepository;
        private IGisInputValueRepository _inputValueRepository;
        private IGisOutputValueRepository _outputValueRepository;
        private IInputFileLogRepository _logFileRepository;
        private UserManager<ApplicationUser> _userManager;
        private int created;
        private int updated;

        public ReviewInputValueController(IGisCountryValueRepository gcValueRepository,
                                          IGisAddonValueRepository addonValueRepository,
                                          IGisInputValueRepository inputValueRepository,
                                          IGisOutputValueRepository outputValueRepository,
                                          IInputFileLogRepository logFileRepository,
                                          UserManager<ApplicationUser> userManager)
        {
            _gcValueRepository = gcValueRepository;
            _addonValueRepository = addonValueRepository;
            _inputValueRepository = inputValueRepository;
            _outputValueRepository = outputValueRepository;
            _logFileRepository = logFileRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewValueListDTO list)
        {
            updated = 0;
            created = 0;
            //определить параметры лога файла
            //var auth = await _authState.GetAuthenticationStateAsync();
            //var user = await _userManager.GetUserAsync(auth.User);
            //var user = await _userManager.GetUserAsync(User);
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            //var user = await _userManager.GetUserAsync(currentUser); //почему-то не работает
            var user = await _userManager.FindByNameAsync(currentUser.Identity.Name);
            DateTime DateFile = DateTime.Now;
            DateTime TimeFile = DateTime.Now;
            var dateFile = StringParser.GetFirstDateFromString(list.Filename);
            var timeFile = StringParser.GetDateWithTimeFromString(list.Filename);
            if (dateFile != null)
            {
                DateFile = dateFile.Value.Date;
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Не удалось определить дату файла",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (timeFile != null)
            {
                TimeFile = timeFile.Value;
            }
            else
            {
                TimeFile = DateFile.AddHours(12);
            }
            var logTime = await _logFileRepository.GetByFilename(list.Filename);
            if (logTime == null)
            {
                logTime = new InputFileLogDTO()
                {
                    Filename = list.Filename,
                    TimeInput = DateTime.Now,
                    DateFile = DateFile,
                    TimeFile = TimeFile,
                    UserId = user.Id
                };
                logTime = await _logFileRepository.Create(logTime);
            }
            else
            {
                logTime.TimeInput = DateTime.Now;
                logTime.UserId = user.Id;
                logTime.TimeFile = TimeFile;
                logTime = await _logFileRepository.Update(logTime);
            }
            foreach (var value in list.Values)
            {
                if (value.inType == ReviewValueInputDTO.InputType.Country)
                {
                    await CreateOrUpdateGcValues(value, logTime);
                }
                else if (value.inType == ReviewValueInputDTO.InputType.Addon)
                {
                    await CreateOrUpdateAddonValues(value, logTime);
                }
                else if (value.inType == ReviewValueInputDTO.InputType.Input)
                {
                    await CreateOrUpdateInputValues(value, logTime);
                }
                else if (value.inType == ReviewValueInputDTO.InputType.Output)
                {
                    await CreateOrUpdateOutputValues(value, logTime);
                }
            }
            var countSaved = new int[] { created, updated };
            return Ok(countSaved);
        }

        private async Task CreateOrUpdateGcValues(ReviewValueInputDTO value, InputFileLogDTO logTime)
        {
            var dbVal = await _gcValueRepository.GetOnDateByGisCountryId(value.ValueId, logTime.DateFile);
            var val = Convert.ToDecimal(Math.Round(value.Value, 8));
            if (dbVal == null)
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted)
                {
                    await _gcValueRepository.Create(new GisCountryValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisCountryId = value.ValueId,
                        RequstedValue = val,
                        RequestedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated)
                {
                    await _gcValueRepository.Create(new GisCountryValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisCountryId = value.ValueId,
                        AllocatedValue = val,
                        AllocatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated)
                {
                    await _gcValueRepository.Create(new GisCountryValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisCountryId = value.ValueId,
                        EstimatedValue = val,
                        EstimatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact)
                {
                    await _gcValueRepository.Create(new GisCountryValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisCountryId = value.ValueId,
                        FactValue = val,
                        FactValueTimeId = logTime.Id
                    });
                    created++;
                }
            }
            else
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted && (dbVal.RequestedValueTime == null || logTime.TimeFile >= dbVal.RequestedValueTime?.TimeFile))
                {
                    dbVal.RequstedValue = val;
                    dbVal.RequestedValueTimeId = logTime.Id;
                    updated++;
                    await _gcValueRepository.Update(dbVal);
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated && (dbVal.AllocatedValueTime == null || logTime.TimeFile >= dbVal.AllocatedValueTime?.TimeFile))
                {
                    dbVal.AllocatedValue = val;
                    dbVal.AllocatedValueTimeId = logTime.Id;
                    updated++;
                    await _gcValueRepository.Update(dbVal);
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated && (dbVal.EstimatedValueTime == null || logTime.TimeFile >= dbVal.EstimatedValueTime?.TimeFile))
                {
                    dbVal.EstimatedValue = val;
                    dbVal.EstimatedValueTimeId = logTime.Id;
                    updated++;
                    await _gcValueRepository.Update(dbVal);
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact && (dbVal.FactValueTime == null ||  logTime.TimeFile >= dbVal.FactValueTime?.TimeFile))
                {
                    dbVal.FactValue = val;
                    dbVal.FactValueTimeId = logTime.Id;
                    updated++;
                    await _gcValueRepository.Update(dbVal);
                }
                
            }
        }

        private async Task CreateOrUpdateAddonValues(ReviewValueInputDTO value, InputFileLogDTO logTime)
        {
            var val = Convert.ToDecimal(Math.Round(value.Value, 8));
            var dbVal = await _addonValueRepository.GetOnDateByGisAddonId(value.ValueId, logTime.DateFile);
            if (dbVal == null)
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted)
                {
                    await _addonValueRepository.Create(new GisAddonValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisAddonId = value.ValueId,
                        RequstedValue = val,
                        RequestedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated)
                {
                    await _addonValueRepository.Create(new GisAddonValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisAddonId = value.ValueId,
                        AllocatedValue = val,
                        AllocatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated)
                {
                    await _addonValueRepository.Create(new GisAddonValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisAddonId = value.ValueId,
                        EstimatedValue = val,
                        EstimatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact)
                {
                    await _addonValueRepository.Create(new GisAddonValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisAddonId = value.ValueId,
                        FactValue = val,
                        FactValueTimeId = logTime.Id
                    });
                    created++;
                }
            }
            else
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted && (dbVal.RequestedValueTime == null || logTime.TimeFile >= dbVal.RequestedValueTime?.TimeFile))
                {
                    dbVal.RequstedValue = val;
                    dbVal.RequestedValueTimeId = logTime.Id;
                    await _addonValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated && (dbVal.AllocatedValueTime == null || logTime.TimeFile >= dbVal.AllocatedValueTime?.TimeFile))
                {
                    dbVal.AllocatedValue = val;
                    dbVal.AllocatedValueTimeId = logTime.Id;
                    await _addonValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated && (dbVal.EstimatedValueTime == null || logTime.TimeFile >= dbVal.EstimatedValueTime?.TimeFile))
                {
                    dbVal.EstimatedValue = val;
                    dbVal.EstimatedValueTimeId = logTime.Id;
                    await _addonValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact && (dbVal.FactValueTime == null || logTime.TimeFile >= dbVal.FactValueTime?.TimeFile))
                {
                    dbVal.FactValue = val;
                    dbVal.FactValueTimeId = logTime.Id;
                    await _addonValueRepository.Update(dbVal);
                    updated++;
                }
            }
        }

        private async Task CreateOrUpdateInputValues(ReviewValueInputDTO value, InputFileLogDTO logTime)
        {
            var val = Convert.ToDecimal(Math.Round(value.Value, 8));
            var dbVal = await _inputValueRepository.GetOnDateByGisId(value.GisId, logTime.DateFile);
            if (dbVal == null)
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted)
                {
                    await _inputValueRepository.Create(new GisInputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        RequstedValue = val,
                        RequestedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated)
                {
                    await _inputValueRepository.Create(new GisInputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        AllocatedValue = val,
                        AllocatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated)
                {
                    await _inputValueRepository.Create(new GisInputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        EstimatedValue = val,
                        EstimatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact)
                {
                    await _inputValueRepository.Create(new GisInputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        FactValue = val,
                        FactValueTimeId = logTime.Id
                    });
                    created++;
                }
            }
            else
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted && (dbVal.RequestedValueTime == null || logTime.TimeFile >= dbVal.RequestedValueTime?.TimeFile))
                {
                    dbVal.RequstedValue = val;
                    dbVal.RequestedValueTimeId = logTime.Id;
                    await _inputValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated && (dbVal.AllocatedValueTime == null || logTime.TimeFile >= dbVal.AllocatedValueTime?.TimeFile))
                {
                    dbVal.AllocatedValue = val;
                    dbVal.AllocatedValueTimeId = logTime.Id;
                    await _inputValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated && (dbVal.EstimatedValueTime == null || logTime.TimeFile >= dbVal.EstimatedValueTime?.TimeFile))
                {
                    dbVal.EstimatedValue = val;
                    dbVal.EstimatedValueTimeId = logTime.Id;
                    await _inputValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact && (dbVal.FactValueTime == null || logTime.TimeFile >= dbVal.FactValueTime?.TimeFile))
                {
                    dbVal.FactValue = val;
                    dbVal.FactValueTimeId = logTime.Id;
                    await _inputValueRepository.Update(dbVal);
                    updated++;
                }
            }
        }

        private async Task CreateOrUpdateOutputValues(ReviewValueInputDTO value, InputFileLogDTO logTime)
        {
            var val = Convert.ToDecimal(Math.Round(value.Value, 8));
            var dbVal = await _outputValueRepository.GetOnDateByGisId(value.GisId, logTime.DateFile);
            if (dbVal == null)
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted)
                {
                    await _outputValueRepository.Create(new GisOutputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        RequstedValue = val,
                        RequestedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated)
                {
                    await _outputValueRepository.Create(new GisOutputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        AllocatedValue = val,
                        AllocatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated)
                {
                    await _outputValueRepository.Create(new GisOutputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        EstimatedValue = val,
                        EstimatedValueTimeId = logTime.Id
                    });
                    created++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact)
                {
                    await _outputValueRepository.Create(new GisOutputValueDTO()
                    {
                        DateReport = value.ReportDate,
                        GisId = value.GisId,
                        FactValue = val,
                        FactValueTimeId = logTime.Id
                    });
                    created++;
                }
            }
            else
            {
                if (value.valType == ReviewValueInputDTO.ValueType.Requsted && (dbVal.RequestedValueTime == null || logTime.TimeFile >= dbVal.RequestedValueTime?.TimeFile))
                {
                    dbVal.RequstedValue = val;
                    dbVal.RequestedValueTimeId = logTime.Id;
                    await _outputValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Allocated && (dbVal.AllocatedValueTime == null || logTime.TimeFile >= dbVal.AllocatedValueTime?.TimeFile))
                {
                    dbVal.AllocatedValue = val;
                    dbVal.AllocatedValueTimeId = logTime.Id;
                    await _outputValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Estimated && (dbVal.EstimatedValueTime == null || logTime.TimeFile >= dbVal.EstimatedValueTime?.TimeFile))
                {
                    dbVal.EstimatedValue = val;
                    dbVal.EstimatedValueTimeId = logTime.Id;
                    await _outputValueRepository.Update(dbVal);
                    updated++;
                }
                else if (value.valType == ReviewValueInputDTO.ValueType.Fact && (dbVal.FactValueTime == null || logTime.TimeFile >= dbVal.FactValueTime?.TimeFile))
                {
                    dbVal.FactValue = val;
                    dbVal.FactValueTimeId = logTime.Id;
                    await _outputValueRepository.Update(dbVal);
                    updated++;
                }
            }
        }
    }
}
