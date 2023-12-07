using CuaHangDienThoaiWebservice.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuaHangDienThoaiWebservice.Connections
{
    public class ConnectManager
    {
        // Lấy tất cả người quản lý
        public Response managerAll(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_manager_all", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Manage> arrayManager = new List<Manage>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Manage mana = new Manage();
                    mana.idql = Convert.ToInt32(dataTable.Rows[i]["idql"]);
                    mana.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    mana.matkhau = Convert.ToString(dataTable.Rows[i]["matkhau"]);
                    mana.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    mana.ngaysinh = Convert.ToDateTime(dataTable.Rows[i]["ngaysinh"]);
                    mana.gioitinh = Convert.ToBoolean(dataTable.Rows[i]["gioitinh"]);
                    mana.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    mana.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    // Gán dữ liệu vào mảng
                    arrayManager.Add(mana);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayManager.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Danh sách tất cả người quản lý";
                response.arrayManage = arrayManager;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được người quản lý nào !";
                response.arrayManage = null;
            }
            return response;
        }

        // Xóa người quàn lý
        public Response deleteManager(SqlConnection connection, int idql)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_delete_manager", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idql", idql);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Xóa người quản lý thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Xóa người quản lý thất bại";
            }
            return response;
        }

        public Response login(Manage manage, SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("sp_login_admin", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@sdt", manage.sdt);
            adapter.SelectCommand.Parameters.AddWithValue("@matkhau", manage.matkhau);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            Response response = new Response();
            List<Manage> arrayManager = new List<Manage>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Manage mana = new Manage();
                    mana.idql = Convert.ToInt32(dataTable.Rows[i]["idql"]);
                    mana.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    mana.matkhau = Convert.ToString(dataTable.Rows[i]["matkhau"]);
                    mana.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    mana.ngaysinh = Convert.ToDateTime(dataTable.Rows[i]["ngaysinh"]);
                    mana.gioitinh = Convert.ToBoolean(dataTable.Rows[i]["gioitinh"]);
                    mana.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    mana.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    // Gán dữ liệu vào mảng
                    arrayManager.Add(mana);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayManager.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Đăng nhập thành công !";
                response.arrayManage = arrayManager;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Đăng nhập thất bại !";
                response.arrayManage = null;
            }
            return response;
        }
        public Response register(Manage manage, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register_admin", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sdt", manage.sdt);
            cmd.Parameters.AddWithValue("@matkhau", manage.matkhau);
            cmd.Parameters.AddWithValue("@hoten", manage.hoten);
            cmd.Parameters.AddWithValue("@ngaysinh", manage.ngaysinh);
            cmd.Parameters.AddWithValue("@gioitinh", manage.gioitinh);
            cmd.Parameters.AddWithValue("@diachi", manage.diachi);
            cmd.Parameters.AddWithValue("@email", manage.email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Đăng ký thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Đăng ký thất bại";
            }
            return response;
        }


        public Response getManagerId(SqlConnection connection, int idsp)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("ql_qlchitiet", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idql", idsp);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            // Mở kết nối
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            List<Manage> arraymanager = new List<Manage>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Manage mana = new Manage();
                    mana.idql = Convert.ToInt32(dataTable.Rows[i]["idql"]);
                    mana.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    mana.matkhau = Convert.ToString(dataTable.Rows[i]["matkhau"]);
                    mana.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    mana.ngaysinh = Convert.ToDateTime(dataTable.Rows[i]["ngaysinh"]);
                    mana.gioitinh = Convert.ToBoolean(dataTable.Rows[i]["gioitinh"]);
                    mana.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    mana.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    // Gán dữ liệu vào mảng
                    arraymanager.Add(mana);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arraymanager.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Quản lý chi tiết";
                response.arrayManage = arraymanager;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được quản lý nào !";
                response.arrayManage = null;
            }
            return response;
        }

        public Response updatemanager(Manage manager, SqlConnection connection, int idql)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("ql_update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idql", idql);
            command.Parameters.AddWithValue("@sdt", manager.sdt);
            command.Parameters.AddWithValue("@matkhau", manager.matkhau);
            command.Parameters.AddWithValue("@hoten", manager.hoten);
            command.Parameters.AddWithValue("@ngaysinh", manager.ngaysinh);
            command.Parameters.AddWithValue("@gioitinh", manager.gioitinh);
            command.Parameters.AddWithValue("@diachi", manager.diachi);
            command.Parameters.AddWithValue("@email", manager.email);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Cập nhật quản lý thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể cập nhật quản lý";
            }

            return response;
        }
    }
}
