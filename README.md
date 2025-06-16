# 🚗 Vehicle Repair Management API

Ứng dụng **ASP.NET Core Web API** quản lý hoạt động sửa chữa phương tiện tại trung tâm bảo dưỡng, xây dựng theo kiến trúc **RESTful**. Dự án đáp ứng yêu cầu bài thi cuối kỳ với các tính năng hiện đại và cấu trúc chuẩn hóa. 📊

---

## 🎯 Mục tiêu

- Xây dựng API **RESTful** quản lý thợ sửa chữa 🧑‍🔧, phương tiện 🚙 và lịch sử sửa chữa 🔧.
- Áp dụng kiến trúc **Service Layer** và **Dependency Injection** 🛠️.
- Tích hợp **Swagger UI** để tài liệu hóa và kiểm tra API 📜.
- Sử dụng **Entity Framework Core** (Code First) để tương tác cơ sở dữ liệu 💾.
- Xử lý lỗi nghiệp vụ với **UserFriendlyException** 🚨.

---

## 📂 Cấu trúc thư mục

```
├── Constants/              # 📌 Hằng số cố định
├── Controllers/            # 🎮 Các API endpoint
├── DbContexts/             # 🔗 Context cho EF Core
├── Dtos/                   # 📬 Data Transfer Objects
│   ├── Mechanic/           # 🧑‍🔧 DTO cho thợ sửa chữa
│   └── Vehicle/            # 🚗 DTO cho phương tiện
├── Entities/               # 🗃️ Các model cơ sở dữ liệu
├── Exceptions/             # ⚠️ Xử lý lỗi tùy chỉnh
├── Migrations/             # 📜 Các file migration EF Core
├── Services/               # 💡 Logic nghiệp vụ
│   ├── Interfaces/         # 📋 Giao diện dịch vụ
│   └── Implements/         # 🛠️ Hiện thực dịch vụ
├── Utils/                  # 🔧 Các tiện ích hỗ trợ
├── Properties/             # ⚙️ Cấu hình dự án
├── appsettings.json        # 📄 Cấu hình ứng dụng
└── Program.cs              # 🚀 Điểm khởi chạy ứng dụng
```

---

## 🗃️ Mô hình dữ liệu

### 🧑‍🔧 Mechanic (Thợ sửa chữa)

| Thuộc tính    | Kiểu dữ liệu | Ràng buộc                     |
|---------------|--------------|-------------------------------|
| `Id`          | int          | 🔑 Khóa chính, tự tăng        |
| `MaTho`       | string       | ✅ Bắt buộc, duy nhất         |
| `TenTho`      | string       | ✅ Bắt buộc, duy nhất         |
| `CCCD`        | string       | ✅ Bắt buộc, duy nhất         |
| `NgayNhanViec`| DateTime     | 📅 Không bắt buộc            |

### 🚙 Vehicle (Phương tiện)

| Thuộc tính    | Kiểu dữ liệu | Ràng buộc                     |
|---------------|--------------|-------------------------------|
| `Id`          | int          | 🔑 Khóa chính, tự tăng        |
| `BienSoXe`    | string       | ✅ Bắt buộc, duy nhất         |
| `LoaiXe`      | string       | ✅ Bắt buộc                   |

### 🔧 RepairRecord (Lịch sử sửa chữa - Quan hệ Many-to-Many)

| Thuộc tính     | Kiểu dữ liệu | Ghi chú                              |
|----------------|--------------|--------------------------------------|
| `Id`           | int          | 🔑 Khóa chính, tự tăng              |
| `MechanicId`   | int          | 🔗 Khóa ngoại tới `Mechanic`        |
| `VehicleId`    | int          | 🔗 Khóa ngoại tới `Vehicle`         |
| `SoLanSua`     | int          | 🔢 Số lần sửa chữa, giá trị >= 0   |

---

## 🌐 API được triển khai

### 🧑‍🔧 Quản lý thợ sửa chữa

| HTTP Method | Endpoint                     | Mô tả                                              |
|-------------|------------------------------|----------------------------------------------------|
| POST        | `/mechanics`                 | ➕ Thêm thợ mới (kiểm tra trùng `MaTho`, `TenTho`, `CCCD`) |
| PUT         | `/mechanics/{id}`            | ✏️ Cập nhật thông tin thợ                           |
| DELETE      | `/mechanics/{id}`            | 🗑️ Xóa thợ                                        |
| GET         | `/mechanics`                 | 📋 Lấy danh sách (hỗ trợ phân trang và lọc)   |

#### 🔍 Tìm kiếm thợ

```json
{
  "keyword": "Nguyen",
  "pageIndex": 1,
  "pageSize": 10
}
```

#### 🚗 Top xe được sửa bởi thợ

| HTTP Method | Endpoint                          | Mô tả                                              |
|-------------|-----------------------------------|----------------------------------------------------|
| GET         | `/mechanics/{id}/top-repaired-vehicles` | 📈 Liệt kê `LoaiXe`, `BienSoXe` có số lần sửa cao nhất |

---

## 🚨 Xử lý lỗi

Sử dụng **UserFriendlyException** để xử lý lỗi nghiệp vụ:

```csharp
public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message) {}
}
```

Trong Controller:

```csharp
try
{
    await _mechanicService.CreateAsync(dto);
}
catch (UserFriendlyException ex)
{
    return BadRequest(new { message = ex.Message });
}
```

---

## ✅ Ràng buộc dữ liệu

- 📏 Sử dụng `[Required]`, `[StringLength]`, `[MaxLength]`, `[Range]` trên DTO.
- ✂️ Tự động `Trim()` các trường string để loại bỏ khoảng trắng thừa.
- 🚫 Không sử dụng thư viện bên ngoài trừ yêu cầu.

---

## 📝 Quy tắc đặt tên

Tên class theo định dạng:  
`<TênClass><MãSốSinhViên>De3<GiờPhútHiệnTại>`  

Ví dụ:  
`MechanicDto20241123De31035`, `RepairRecordController20241123De31035`

⚠️ **Lưu ý**: Không áp dụng cho file trong thư mục `Migrations/`.

---

## 🛠️ Công nghệ sử dụng

- **ASP.NET Core 8 Web API** 🌐: Xây dựng API RESTful.
- **Swagger / Swashbuckle** 📖: Tài liệu hóa API.
- **Entity Framework Core (Code First)** 💾: Quản lý cơ sở dữ liệu.
- **LINQ & Query Syntax** 🔍: Truy vấn dữ liệu hiệu quả.
- **Service Layer + Interface** 🗂️: Tách biệt logic nghiệp vụ.
- **Exception Handling** 🚨: Xử lý lỗi chuyên nghiệp.
- **SQL Server** 🗄️: Cơ sở dữ liệu mạnh mẽ.

---

## 🚀 Hướng dẫn chạy ứng dụng

1. Tạo migration ban đầu:
   ```bash
   dotnet ef migrations add InitialCreate
   ```

2. Cập nhật cơ sở dữ liệu:
   ```bash
   dotnet ef database update
   ```

3. Chạy ứng dụng:
   ```bash
   dotnet run
   ```

4. Truy cập **Swagger UI** tại:  
   🌐 `https://localhost:{PORT}/swagger`

---

## 📌 Lưu ý

- 🚫 Không sử dụng thư viện bên ngoài trừ yêu cầu.
- ✅ Đảm bảo đặt tên namespace và class đúng quy định.
- ⏰ Hoàn thành ứng dụng trong 120 phút.
