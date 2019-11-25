using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HoteliTest.DTO;
using HoteliTest.Models;

namespace HoteliTest.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Hotel, HotelDTO>().ReverseMap();
                cfg.CreateMap<TipSobe, TipSobeDTO>().ReverseMap();
                cfg.CreateMap<Soba, SobaDTO>().ReverseMap();
                cfg.CreateMap<Gost, GostDTO>().ReverseMap();
                cfg.CreateMap<Usluga, UslugaDTO>().ReverseMap();
                cfg.CreateMap<Rezervacija, RezervacijaDTO>().ReverseMap();
                cfg.CreateMap<Racun, RacunDTO>().ReverseMap();
                cfg.CreateMap<StavkaRacuna, StavkaRacunaDTO>().ReverseMap();

            });
        }
    }
}