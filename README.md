# wmvn-merchant-csharp
Webmoney Vietnam Merchant C# Sample Code

Đây là thư viện sample code để tích hợp giao tiếp với cổng thanh toán Webmoney Merchant API, dành cho các đối tác của Webmoney Vietnam

Yêu cầu
------------
- .NET Framework 4.5.2

Hướng dẫn sử dụng
-----------------------
Build project WM.Merchant. Add reference vào project web của bạn 
Thư viện sẽ yêu cầu một số thông tin chứng thực API, bao gồm MerchantCode, Passcode và Secret Key, được cung cấp bởi Webmoney 

Thêm và chỉnh sửa một số thông tin trong Web.config

```csharp

<configuration>
  	<configSections>
    	...
    	<section name="wmMerchant" type="WM.Merchant.MerchantConfiguration" />
  	</configSections>
	...
	<wmMerchant>
    	<wmService merchantCode="YOUR MERCHANT CODE" passcode="YOUR PASSCODE" secretKey="YOUR SECRET KEY" productionMode="false" />
  	</wmMerchant>
<configuration>
```

Class WMService gồm có một số phương thức chính:
```csharp
public WMResponseHandler<CreateOrderResponse> CreateOrder(CreateOrderRequest model);
```

Gửi HTTP POST đến Webmoney Merchant để tạo đơn hàng. Thông tin trả về bao gồm Transaction ID của giao dịch trên Webmoney và RedirectURL để chuyển đến cổng thanh toán

```csharp
public WMResponseHandler<ViewOrderResponse> ViewOrder(ViewOrderRequest model);
```

Gửi HTTP POST đến Webmoney Merchant để xem thông tin giao dịch.

```csharp
public string ValidateSuccessURL();
public string ValidateFailedURL();
public string ValidateCanceledURL();
```

Sau khi thanh toán thành công, hoặc giao dịch bị hủy. Cổng thành toán sẽ trả về URL của đối tác, bao gồm transaction ID của đơn hàng và checksum. Lúc đó cần sử dụng những phương thức này để kiểm tra
