Vehicle Repair Management API
Ứng dụng API quản lý hoạt động sửa chữa phương tiện giao thông tại trung tâm bảo dưỡng, được xây dựng bằng ASP.NET Core Web API theo kiến trúc RESTful. Dự án đáp ứng các yêu cầu của bài thi cuối kỳ với các tính năng chính và cấu trúc chuẩn hóa.

Mục tiêu

Xây dựng API RESTful quản lý thông tin thợ sửa chữa, phương tiện và lịch sử sửa chữa.
Áp dụng kiến trúc Service Layer và Dependency Injection.
Tích hợp Swagger UI để tài liệu hóa và kiểm tra API.
Sử dụng Entity Framework Core (Code First) để tương tác cơ sở dữ liệu.
Xử lý lỗi nghiệp vụ với UserFriendlyException.


Cấu trúc thư mục
├── Constants/              # Các hằng số cố định
├── Controllers/            # Các API endpoint
├── DbContexts/             # Context cho Entity Framework Core
├── Dtos/                   # Data Transfer Objects
│   ├── Mechanic/           # DTO cho thợ sửa chữa
│   └── Vehicle/            # DTO cho phương tiện
├── Entities/               # Các model cơ sở dữ liệu
├── Exceptions/             # Xử lý lỗi tùy chỉnh
├── Migrations/             # Các file migration của EF Core
├── Services/               # Logic nghiệp vụ
│   ├── Interfaces/         # Giao diện dịch vụ
│   └── Implements/         # Hiện thực dịch vụ
├── Utils/                  # Các tiện ích hỗ trợ
├── Properties/             # Cấu hình dự án
├── appsettings.json        # Cấu hình ứng dụng
└── Program.cs              # Điểm khởi chạy ứng dụng


Mô hình dữ liệu
Mechanic (Thợ sửa chữa)



Thuộc tính
Kiểu dữ liệu
Ràng buộc



Id
int
Khóa chính, tự tăng


MaTho
string
Bắt buộc, duy nhất


TenTho
string
Bắt buộc, duy nhất


CCCD
string
Bắt buộc, duy nhất


NgayNhanViec
DateTime
Không bắt buộc


Vehicle (Phương tiện)



Thuộc tính
Kiểu dữ liệu
Ràng buộc



Id
int
Khóa chính, tự tăng


BienSoXe
string
Bắt buộc, duy nhất


LoaiXe
string
Bắt buộc


RepairRecord (Lịch sử sửa chữa - Quan hệ Many-to-Many)



Thuộc tính
Kiểu dữ liệu
Ghi chú



Id
int
Khóa chính, tự tăng


MechanicId
int
Khóa ngoại tới Mechanic


VehicleId
int
Khóa ngoại tới Vehicle


SoLanSua
int
Số lần sửa chữa, giá trị >= 0



API được triển khai
Quản lý thợ sửa chữa



HTTP Method
Endpoint
Mô tả



POST
/mechanics
Thêm thợ mới (kiểm tra trùng MaTho, TenTho, CCCD)


PUT
/mechanics/{id}
Cập nhật thông tin thợ


DELETE
/mechanics/{id}
Xóa thợ sửa chữa


GET
/mechanics
Lấy danh sách thợ (hỗ trợ phân trang và lọc theo từ khóa)


Lọc danh sách thợ
{
  "keyword": "Nguyen",
  "pageIndex": 1,
  "pageSize": 10
}

Lấy danh sách xe được sửa nhiều nhất bởi một thợ



HTTP Method
Endpoint
Mô tả



GET
/mechanics/{id}/top-repaired-vehicles
Trả về danh sách LoaiXe, BienSoXe có số lần sửa cao nhất của thợ



Xử lý lỗi
Sử dụng custom exception để xử lý lỗi nghiệp vụ:
public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message) {}
}

Trong Controller:
try
{
    await _mechanicService.CreateAsync(dto);
}
catch (UserFriendlyException ex)
{
    return BadRequest(new { message = ex.Message });
}


Ràng buộc dữ liệu

Sử dụng các attribute [Required], [StringLength], [MaxLength], [Range] trên các DTO.
Tự động loại bỏ khoảng trắng thừa (Trim()) cho các trường string.
Không sử dụng thư viện bên ngoài trừ những thư viện được yêu cầu.


Quy tắc đặt tên
Tên class tuân theo định dạng:<TênClass><MãSốSinhViên>De3<GiờPhútHiệnTại>  
Ví dụ:MechanicDto20241123De31035, RepairRecordController20241123De31035
Lưu ý: Quy tắc này không áp dụng cho các file trong thư mục Migrations/.

Công nghệ sử dụng

ASP.NET Core 8 Web API: Xây dựng API RESTful.
Swagger / Swashbuckle: Tài liệu hóa và kiểm tra API.
Entity Framework Core (Code First): Quản lý cơ sở dữ liệu.
LINQ & Query Syntax: Truy vấn dữ liệu hiệu quả.
Service Layer + Interface: Tách biệt logic nghiệp vụ.
Exception Handling: Xử lý lỗi chuyên nghiệp.
SQL Server: Cơ sở dữ liệu.


Hướng dẫn chạy ứng dụng

Tạo migration ban đầu:
dotnet ef migrations add InitialCreate


Cập nhật cơ sở dữ liệu:
dotnet ef database update


Chạy ứng dụng:
dotnet run


Truy cập Swagger UI tại:https://localhost:{PORT}/swagger



Lưu ý

Không sử dụng thư viện bên ngoài trừ những thư viện được yêu cầu.
Đảm bảo đặt tên namespace và class đúng theo quy định.
Hoàn thành ứng dụng trong thời gian 120 phút.

