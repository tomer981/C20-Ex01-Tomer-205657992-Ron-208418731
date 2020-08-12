using System;
using FacebookWrapper.ObjectModel;

namespace C20_Ex01_TomerAbutbul_205657992_RonJourno_208418731
{
    public class CheckinDetails
    {
        private Location m_Location;
        public CheckinDetails(Checkin i_Checkin)
        {
            this.CreatedTime = i_Checkin.CreatedTime;
            //this.From =
            this.Message = i_Checkin.Message;
            //this.Status = 
            this.UpdateTime = i_Checkin.UpdateTime;
            //this.City = i_Checkin.Place.Location.City;
            //this.m_Location = new Location();

        }
        public string Message { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public string From { get; set; }

    }
}