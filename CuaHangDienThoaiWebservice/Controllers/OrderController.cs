using CuaHangDienThoaiWebservice.Connections;
using CuaHangDienThoaiWebservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CuaHangDienThoaiWebservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration; // Khởi tạo cấu hình và tên cấu hình

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Khởi tạo API insert thông tin đơn hàng 
        [HttpPost]
        [Route("order")]
        public Response Order(Order order)
        {
            Response response = new Response();
            ConnectOrder connectOrder = new ConnectOrder();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("webservice").ToString());
            response = connectOrder.order(order, connection);
            return response;
        }

        // Khởi tạo API lấy iddh đơn hàng mới nhất
        [HttpGet]
        [Route("orderiddh")]
        public Response OrderIDdh()
        {
            Response response = new Response();
            ConnectOrder connectOrder = new ConnectOrder();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("webservice").ToString());
            response = connectOrder.orderiddh(connection);
            return response;
        }

        [HttpGet]
        [Route("orderIdkh")]
        public Response OrderIdkh(int idkh)
        {
            Response response = new Response();
            ConnectOrder connectOrder = new ConnectOrder();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("webservice").ToString());
            response = connectOrder.getOderByIdkh(connection, idkh);
            return response;
        }

        [HttpGet]
        [Route("getOrder")]
        public Response getOrder()
        {
            Response response = new Response();
            ConnectOrder connectOrder = new ConnectOrder();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("webservice").ToString());
            response = connectOrder.orderAll(connection);
            return response;
        }
    }
}
