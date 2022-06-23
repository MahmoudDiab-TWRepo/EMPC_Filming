using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS
{
    public class RequestStatus
    {
        public string Message { get; set; }
        public Status Status { get; set; }
    }
    public class ManageRequestStatus
    {
        public RequestStatus GetStatus(Status status, string message = null)
        {
            RequestStatus requestStatus = new RequestStatus();
            requestStatus.Status = status;
            if (message == null)
            {

                switch (status)
                {
                    case Status.Created:
                        requestStatus.Message = "Added successfully";
                        break;
                    case Status.Edited:
                        requestStatus.Message = "Edited successfully";
                        break;
                }
            }
            else
                requestStatus.Message = message;
            return requestStatus;
        }
    }

    public enum Status
    {
        Edited = 201,
        Created = 200,
        GeneralError = 500
    }
    public static class Shared
    {
        public static DateTime GetDateTime()
        {
            DateTime utcTime = DateTime.UtcNow;

            string zoneID = "UTC";

            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById(zoneID);
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            return custDateTime.AddHours(2);
        }
        //Use Student Online and Acadmic 

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        static string[] imgs_ex = { "jpg", "peg", "png", "gif", "jpeg" };
        static string[] video_ex = { "m4v", "avi", "mpg", "mp4" };

        public static bool CheckImageExtention(this string ex)
        {
            ex = ex.Replace("image/", "");
            return imgs_ex.Contains(ex.ToLower());
        }
        public static bool CheckVideoExtention(this string ex)
        {
            ex = ex.Replace(".", "").Replace("video/", "");
            bool flag = video_ex.Contains(ex.ToLower());

            return flag;
        }


    }
    public enum Type
    {
        Suggestions, Complaints
    }
}