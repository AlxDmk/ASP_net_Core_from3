using System;
using System.Data.Common;

namespace Lesson3.DAL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DeleteDate { get; set; }

        public void Delete()
        {
            if (IsDelete)
            {
                return;
            }

            IsDelete = true;
            DeleteDate = DateTime.Now;
        }
    }
}