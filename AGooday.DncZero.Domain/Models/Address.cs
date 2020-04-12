using AGooday.DncZero.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 地址
    /// </summary>
    [Owned]
    public class Address : ValueObject<Address>
    {
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// 详细
        /// </summary>
        public string Detailed { get; private set; }

        public Address() { }

        public Address(string province, string city,
            string county, string street, string detailed = null)
        {
            this.Province = province;
            this.City = city;
            this.County = county;
            this.Street = street;
            this.Detailed = detailed;
        }

        protected override bool EqualsCore(Address other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
