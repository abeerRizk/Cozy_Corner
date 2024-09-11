using AutoMapper; // Ensure AutoMapper is being used
using ITI_Project.BLL.ModelVM;
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

        public VendorService(IVendorRepo vendorRepo, IMapper mapper) 
        {
            _vendorRepo = vendorRepo;
            _mapper = mapper; 
        }

        public IEnumerable<GetAllVendorVM> GetAllVendors()
        {
            try
            {
                var vendors = _vendorRepo.GetAllVendors();

                var vendorVMs = _mapper.Map<IEnumerable<GetAllVendorVM>>(vendors);

                return vendorVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<GetAllVendorVM>();
            }
        }

        public Vendor GetVendorById(int id)
        {
            return _vendorRepo.GetVendorById(id);
        }

        public (bool sucess, string message) AddVendor(CreateVendorVM vendorVM)
        {
            try
            {
                

                var vendorEntity = _mapper.Map<Vendor>(vendorVM);
                if (_vendorRepo.IsEmailExist(vendorEntity.Email))
                    return (false, "Email is already exist");


                return (_vendorRepo.AddVendor(vendorEntity),string.Empty);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
                return (false,ex.Message);
            }
        }

        public bool DeleteVendor(int id)
        {
            try
            {
                var vendor = _vendorRepo.GetVendorById(id);
                if (vendor == null)
                {
                    Console.WriteLine("Vendor not found");
                    return false;
                }

                _vendorRepo.DeleteVendor(id); 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateVendor(UpdateVendorVM vendorVM)
        {
            try
            {
                var vendor = _vendorRepo.GetVendorById(vendorVM.Id);
                if (vendor == null)
                {
                    Console.WriteLine("Vendor not found");
                    return false;
                }

                _mapper.Map(vendorVM, vendor);

                _vendorRepo.UpdateVendor(vendor);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
