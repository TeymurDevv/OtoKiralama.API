﻿using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Car : BaseEntity
    {
        public string Plate { get; set; }
        public int SeatCount { get; set; }
        public double DailyPrice { get; set; }
        public int Year { get; set; }
        public bool IsInstantConfirm { get; set; } // Rezervasyon onayı gerekli mi?
        public bool IsFreeRefund { get; set; } // İptal durumunda ücretsiz geri ödeme var mı?
        public bool IsActive { get; set; } = true;
        public bool IsReserved { get; set; } = false;
        public int BodyId { get; set; }
        public Body Body { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }
        public int GearId { get; set; }
        public Gear Gear { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        //yeni yaradilan entitiler
        public bool IsLimited { get; set; } = false;
        public double? Limit { get; set; }
        public int DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public double DepositAmount { get; set; }


        // Rezervasyonlar
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();


    }
}
