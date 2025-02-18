﻿using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Service
{
    public class HotelService
    {
        private readonly IHotelRepository _hotelRepository;
        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<int> CreateHotel(Hotel hotel)
        {
            return await _hotelRepository.Create(hotel);
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            return (List<Hotel>)await _hotelRepository.GetAll();
        }

        public async Task<Hotel> GetById(int Id)
        {
            return await _hotelRepository.GetById(Id);
        }

        public async Task<Hotel> Update(Hotel hotel)
        {
            return await _hotelRepository.UpdateReturn(hotel);
        }

        public async Task<List<Hotel>> SearchHotels(DateOnly checkIn, DateOnly checkOut, int guests, string city)
        {
            return await _hotelRepository.SearchHotels(checkIn, checkOut, guests, city);
        }
    }
}
