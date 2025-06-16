# 🛠️ Vehicle Repair Management API - Final Exam

> 👨‍💻 Họ tên: *[Your Name]*  
> 🆔 MSSV: *[Your Student ID]*  
> ⏱️ Thời gian làm bài: 120 phút  
> 📅 Đề số: 2  

---

## 🎯 Mục tiêu

Xây dựng ứng dụng ASP.NET Core Web API theo kiến trúc **RESTful**, quản lý hoạt động sửa chữa phương tiện giao thông tại trung tâm bảo dưỡng. Dự án có:

- Cấu trúc chuẩn hoá thư mục & naming convention
- Áp dụng chuẩn `Service Layer`, `Dependency Injection`
- Tích hợp Swagger UI
- Tương tác cơ sở dữ liệu bằng **Entity Framework Core** (Code First)
- Xử lý lỗi bằng `UserFriendlyException`

---

## 📁 Cấu trúc thư mục

├── Constants/
├── Controllers/
├── DbContexts/
├── Dtos/
│ ├── Mechanic/
│ └── Vehicle/
├── Entities/
├── Exceptions/
├── Migrations/
├── Services/
│ ├── Interfaces/
│ └── Implements/
├── Utils/
├── Properties/
├── appsettings.json
└── Program.cs


---

## 🧱 Mô hình dữ liệu

### 🧑 Mechanic

| Thuộc tính    | Kiểu dữ liệu | Ràng buộc                 |
|---------------|--------------|----------------------------|
| `Id`          | int          | PK, tự tăng                |
| `MaTho`       | string       | Bắt buộc, duy nhất         |
| `TenTho`      | string       | Bắt buộc, duy nhất         |
| `CCCD`        | string       | Bắt buộc, duy nhất         |
| `NgayNhanViec`| DateTime     |                            |

---

### 🚗 Vehicle

| Thuộc tính    | Kiểu dữ liệu | Ràng buộc                 |
|---------------|--------------|----------------------------|
| `Id`          | int          | PK, tự tăng                |
| `BienSoXe`    | string       | Bắt buộc, duy nhất         |
| `LoaiXe`      | string       | Bắt buộc                   |

---

### 🔧 RepairRecord (Many-to-Many)

| Thuộc tính     | Kiểu dữ liệu | Ghi chú                                |
|----------------|--------------|----------------------------------------|
| `Id`           | int          | PK, tự tăng                            |
| `MechanicId`   | int          | FK tới `Mechanic`                      |
| `VehicleId`    | int          | FK tới `Vehicle`                       |
| `SoLanSua`     | int          | >= 0                                   |

---

## 🚀 API yêu cầu

### ✅ Thợ sửa chữa

| Hành động                  | Mô tả |
|----------------------------|------|
| `POST /mechanics`          | Thêm mới (check trùng `MaTho`, `TenTho`, `CCCD`) |
| `PUT /mechanics/{id}`      | Cập nhật thông tin |
| `DELETE /mechanics/{id}`   | Xoá thợ sửa chữa |
| `GET /mechanics`           | Lấy danh sách (phân trang + filter theo keyword) |

#### 📥 Lọc danh sách
```json
{
  "keyword": "Nguyen",
  "pageIndex": 1,
  "pageSize": 10
}
✅ Xe được sửa nhiều nhất bởi 1 thợ
API	Mô tả
GET /mechanics/{id}/top-repaired-vehicles	Trả về danh sách LoaiXe, BienSoXe có SoLanSua = MAX của mechanic đó

⚠️ Xử lý lỗi nghiệp vụ
Tạo custom exception:

csharp
Sao chép
Chỉnh sửa
public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message) {}
}
Trong Controller:

csharp
Sao chép
Chỉnh sửa
try
{
    await _mechanicService.CreateAsync(dto);
}
catch (UserFriendlyException ex)
{
    return BadRequest(new { message = ex.Message });
}
🧪 Ràng buộc dữ liệu
Áp dụng trên các DTO:

[Required], [StringLength], [MaxLength], [Range]

Tự động Trim() các trường string

Không dùng thư viện ngoài

🔤 Quy tắc đặt tên
Format:

php-template
Sao chép
Chỉnh sửa
<TênClass><MãSốSinhViên>De3<GiờPhútHiệnTại>
Ví dụ:
MechanicDto20241123De31035, RepairRecordController20241123De31035

⚠️ Không áp dụng với file trong thư mục Migrations/

🧠 Kỹ thuật & Công nghệ
ASP.NET Core 8 Web API

Swagger / Swashbuckle

EF Core (Code First)

LINQ & Query Syntax

Service Layer + Interface

Exception Handling

SQL Server

▶️ Hướng dẫn chạy ứng dụng
bash
Sao chép
Chỉnh sửa
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
Truy cập Swagger UI tại:
👉 https://localhost:{PORT}/swagger

📝 Ghi chú
Không sử dụng thư viện ngoài nếu đề không yêu cầu.

Đặt tên namespace và class đúng theo quy định.

Làm bài trong thời gian 120 phút, không được giải thích thêm.






