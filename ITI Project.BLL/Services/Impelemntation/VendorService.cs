using AutoMapper; // Ensure AutoMapper is being used
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Impelemntation;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;


namespace ITI_Project.BLL.Services.Implementation
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepo _vendorRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public VendorService(IVendorRepo vendorRepo, IMapper mapper, IUserService userService)
        {
            _vendorRepo = vendorRepo;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task <IEnumerable<GetAllVendorVM>> GetAllVendors()
        {
            try
            {
                var vendors = await  _vendorRepo.GetAllVendors();

                var vendorVMs = _mapper.Map<IEnumerable<GetAllVendorVM>>(vendors);

                return vendorVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<GetAllVendorVM>();
            }
        }

        public async Task<Vendor> GetVendorById(int id)
        {
            return await  _vendorRepo.GetVendorById(id);
        }

        public async Task<(bool sucess, string message)> AddVendor(CreateVendorVM vendorVM)
        {
            try
            {
                

                var vendorEntity = _mapper.Map<Vendor>(vendorVM);
                if (await _vendorRepo.IsEmailExist(vendorEntity.Email))
                    return (false, "Email is already exist");


                return ( await _vendorRepo.AddVendor(vendorEntity),string.Empty);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
                return (false,ex.Message);
            }
        }



        public async Task <bool> UpdateVendor(UpdateVendorVM vendorVM)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(vendorVM.userId);
                user.UserName = vendorVM.Name;
                user.PhoneNumber = vendorVM.Phone_Number;
                user.address = vendorVM.Location;
                Vendor new_vendor = _mapper.Map<Vendor>(vendorVM);
                new_vendor.Id = vendorVM.Id;
                await _vendorRepo.UpdateVendor(new_vendor);
                await _userService.UpdateUserAsync(user);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task< int> GetVendorId_ByUserId(string userId)
        {

            return await _vendorRepo.GetVendorId_ByUserId(userId);
        }
    }
}
