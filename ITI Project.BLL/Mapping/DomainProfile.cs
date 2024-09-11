using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.DAL.Entites;

namespace ITI_Project.BLL.Mapping
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {

            CreateMap<CreateVendorVM, Vendor>();
            CreateMap<Vendor, GetAllVendorVM>();
            CreateMap<Vendor, UpdateVendorVM>().ReverseMap();
            CreateMap<UpdateVendorVM, Vendor>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap <CreateProductVM, Product>();
            CreateMap<Product, CreateProductVM>();

            CreateMap<GetProductVM, Product>();
            CreateMap<Product, GetProductVM>();

            CreateMap<UpdateProductVM, Product>();
            CreateMap<Product, UpdateProductVM>();
            CreateMap<Order,OrderModelVM>().ReverseMap();

            CreateMap<UpdateProductVM, GetProductVM>();
            CreateMap<GetProductVM, UpdateProductVM>();

            CreateMap<CreateCustomerVM, Customer>();
            CreateMap<Customer, CreateCustomerVM>();

            CreateMap<GetCustomerVM, Customer>();
            CreateMap<Customer, GetCustomerVM>();

            CreateMap<UpdateCustomerVM, Customer>();
            CreateMap<Customer, UpdateCustomerVM>();


            CreateMap<UpdateCustomerVM, GetCustomerVM>();
            CreateMap<GetCustomerVM, UpdateCustomerVM>();


            CreateMap<OrderItem,OrderItemsVM>().ReverseMap();


            CreateMap<Rating , CreateRatingVM>();
            CreateMap<CreateRatingVM, Rating > ();

            CreateMap<Rating , UpdateRatingVM>();
            CreateMap<UpdateRatingVM, Rating>();


            CreateMap<Rating , GetRatingVM>();
            CreateMap<GetRatingVM, Rating>();

            CreateMap<GetRatingVM, UpdateRatingVM>();
            CreateMap<UpdateRatingVM, GetRatingVM>();


            CreateMap<Invoice, CreateInvoiceVM>();
            CreateMap<CreateInvoiceVM, Invoice>();

            CreateMap<Invoice, UpdateInvoiceVM>();
            CreateMap<UpdateInvoiceVM, Invoice>();


            CreateMap<Invoice, GetInvoiceVM>();
            CreateMap<GetInvoiceVM, Invoice>();

            CreateMap<GetInvoiceVM, UpdateInvoiceVM>();
            CreateMap<UpdateInvoiceVM, GetInvoiceVM>();

            CreateMap<Notification, AddNotificationVM>().ReverseMap();
            CreateMap<Follow, FollowVM>();
            CreateMap<FollowVM, Follow>();


        }
    }
}
