# AccServer / SmartAccCloud

Backend ASP.NET Core (.NET 9) cho kế toán, nhân sự và quản lý doanh nghiệp.

## Chạy trên máy local

1. Cài .NET SDK 9. Máy hiện tại đã có SDK và SQL Server LocalDB.
2. Khôi phục database `SMARTDBMSSEC` và các database nghiệp vụ từ bộ backup/script của dự án vào `(localdb)\MSSQLLocalDB`. MySQL không thay thế được SQL Server trong mã nguồn hiện tại.
3. Chạy Redis ở `localhost:6379`. Nếu có Docker Compose, dùng:

   ```powershell
   docker compose -f compose.local.yml up -d
   ```

   Máy hiện tại chưa có Docker/Redis trong các vị trí đã kiểm tra. Cần cài Docker hoặc một dịch vụ Redis tương thích trước khi dùng lệnh này.

4. Từ thư mục repository, restore và build:

   ```powershell
   dotnet restore SmartAccCloudServer.sln
   dotnet build SmartAccCloudServer.sln --no-restore -m:1
   ```

5. Trong Visual Studio, chọn profile nhiều project trong `SmartAccCloudServer.slnLaunch.user`, dùng launch profile `https`. Profile này chạy Catalog, Identity, Systems và Web Gateway. Hoặc mở bốn terminal và chạy lần lượt:

   ```powershell
   dotnet run --project src/Modules/Catalog/Catalog --no-build --launch-profile https
   dotnet run --project src/Modules/Identity/Identity --no-build --launch-profile https
   dotnet run --project src/Modules/Systems/Systems --no-build --launch-profile https
   dotnet run --project src/ApiGateways/Web.Bff.ApiGateway --no-build --launch-profile https
   ```

   Nếu HTTPS chưa được tin cậy, chạy `dotnet dev-certs https --trust`.

Web Gateway: `https://localhost:7263`. Các dịch vụ khác (Voucher, Report, FileHandle, v.v.) cần chạy riêng khi sử dụng các chức năng tương ứng. Gateway Development chuyển Identity về dịch vụ local ở `https://localhost:5001`.

Các file `appsettings.Development.json` của module cấu hình LocalDB, Redis local và JWT issuer local. Có thể ghi đè bằng biến môi trường `ConnectionStrings__MultitenantConnection`, `ConnectionStrings__Redis`, `JwtSettings__Authority` khi dùng instance khác. Những cấu hình này áp dụng khi `ASPNETCORE_ENVIRONMENT=Development`.

## Kiểm tra trạng thái

- `/alive`: tiến trình API đang hoạt động.
- `/health`: kiểm tra cả Redis và database tenant. HTTP 503 khi dependency chưa sẵn sàng.

API có thể khởi động khi Redis tạm ngắt kết nối; các thao tác cần Redis vẫn yêu cầu Redis hoạt động. Khi chưa có database/schema và dữ liệu tenant, đăng nhập và các API nghiệp vụ chưa thể sử dụng. Repository hiện không có bộ migration/script đầy đủ để dựng lại các database này; cần lấy backup/schema và stored procedure từ người quản lý dự án. Kết nối database theo từng tenant được lưu trong bảng `InformationCustommer`, nên sau khi restore cần cấu hình lại cho môi trường local.

Nếu có lỗi khóa `.exe`/`.dll` khi build, dừng phiên debug hoặc dịch vụ tương ứng rồi build lại. Nếu gặp đường dẫn build từ máy cũ, chạy `dotnet clean SmartAccCloudServer.sln`, `dotnet restore` và build lại. `bin`, `obj`, `.vs` là dữ liệu sinh tự động và không nên đưa vào các commit mới; `.gitignore` không tự bỏ theo dõi những file đã được commit trước đó.

`Directory.Build.props` tắt riêng cache của task `DefineStaticWebAssets` để tránh lỗi `user-mapped section open` khi SDK ghi `rpswa.dswa.cache.json`. Static assets vẫn được tạo; bước này có thể tốn thêm thời gian build vì không tái sử dụng cache.
