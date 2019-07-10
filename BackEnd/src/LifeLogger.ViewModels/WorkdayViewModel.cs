using LifeLogger.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLogger.ViewModels
{
    public class WorkdayViewModel
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public TimeSpan WorkTime { get; set; }
        public float IncPerHour { get; set; }
        public float IncMade { get; set; }


        public static implicit operator WorkdayViewModel(Workday workday)
        {
            return new WorkdayViewModel
            {
                Id = workday.Id,
                StartDate = workday.StartDate,
                EndDate = workday.EndDate,
                Comment = workday.Comment,
                WorkTime = workday.WorkTime,
                IncPerHour = workday.IncPerHour,
                IncMade = workday.IncMade


            };
        }

        public static implicit operator Workday(WorkdayViewModel vm)
        {
            Workday wd = new Workday
            {
                Id = vm.Id,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                Comment = vm.Comment,
                WorkTime = vm.EndDate - vm.StartDate,
                IncPerHour = vm.IncPerHour,
            };
            wd.IncMade = (float)wd.WorkTime.TotalHours * wd.IncPerHour;
            return wd;
        }
    }
}
