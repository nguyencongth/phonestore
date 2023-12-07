Create DataBase WebService
/*
  CONSTRAINT [FK_Products_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId])
  ON DELETE CASCADE /* Khi xóa Category, các sản phẩm thuộc về nó cũng bị xóa theo */
  ON DELETE SET NULL nếu một khách hàng bị xóa khỏi bảng Khách hàng, tất cả các khóa ngoại tương ứng trong bảng Đơn hàng sẽ tự động được đặt thành NULL.

  CONSTRAINT [FK_Products_Category_CateID]
             FOREIGN KEY ([CateID]) REFERENCES [Category] ([CategoryId])  /* ràng buộc đến Category */
             ON DELETE NO ACTION /* khi xóa Category thì Product không thay đổi gì*/
*/
/*
KhachHang (idkh, sdt, matkhau, hoten, gioitinh, diachi, email)
QuanLi (idql, sdt, matkhau, email, hoten, gioitinh, diachi)
LoaiSP (idloai, tenloai)
SanPham (idsp, idloai, idkho, anhsp, tensp, giasp, thongtinsp, slsanpham)
GioHang (idgh, idkh, idsp, slchon)
DonHang (iddh, idkh, hoten, diachi, sdt, email, ngaylap)
ChiTietDonHang (iddh, idsp, sldamua)
*/

/* Tạo bảng Tài Khoản Quản Lý */
Create Table QuanLy(
idql int NOT NUll IDENTITY(1,1), /*Không cho khóa trống và để khóa tự tăng bắt đầu bằng 1 và tăng lên 1*/
sdt varchar(20), /* Sử dụng NOT NULL yêu cầu dòng dữ liệu không được để trống */
matkhau varchar(50),
hoten nvarchar(100),
ngaysinh date,
gioitinh tinyint,
diachi nvarchar(50),
email nvarchar(100)
Constraint PK_QuanLy Primary Key (idql)
)

/* Tạo bảng Tài Khoản Khách Hàng */
Create Table KhachHang(
idkh int NOT NUll IDENTITY(1,1), /*Không cho khóa trống và để khóa tự tăng bắt đầu bằng 1 và tăng lên 1*/
sdt varchar(20), /* Sử dụng NOT NULL yêu cầu dòng dữ liệu không được để trống */
matkhau varchar(50),
hoten nvarchar(100),
ngaysinh date,
gioitinh tinyint,
diachi nvarchar(50),
email nvarchar(100)
Constraint PK_KhachHang Primary Key (idkh)
)

/* Tạo bảng Loại Sản Phẩm */
Create Table LoaiSP(
idloai int NOT NUll IDENTITY(1,1),
tenloai nvarchar(100)
Constraint PK_LoaiSP Primary Key (idloai)
)

/* Tạo bảng Sản Phẩm */
Create Table SanPham(
idsp int NOT NUll IDENTITY(1,1),
idloai int,
anhsp varchar(100),
tensp nvarchar(100),
giasp int,
thongtinsp nvarchar(1000),
slsanpham int
Constraint PK_SanPham Primary Key (idsp)
Constraint PK_SanPham_LoaiSanPham Foreign Key (idloai) References LoaiSP(idloai) ON DELETE CASCADE 
)

Alter Table SanPham



/* Tạo bảng Giỏ Hàng */
Create Table GioHang(
idgh int NOT NUll IDENTITY(1,1),
idkh int,
idsp int,
slchon int
Constraint PK_GioHang Primary Key (idgh)
Constraint PK_GioHang_KhachHang Foreign Key (idkh) References KhachHang(idkh) ON DELETE CASCADE,
Constraint PK_GioHang_SanPham Foreign Key (idsp) References SanPham(idsp) ON DELETE CASCADE
)

/* Tạo bảng Đơn Hàng */
Create Table DonHang(
iddh int NOT NUll IDENTITY(1,1),
idkh int,
idql int,
hoten nvarchar(100),
diachi nvarchar(50),
sdt varchar(20),
email nvarchar(100),
ngaylap date
Constraint PK_DonHang Primary Key (iddh)
Constraint PK_DonHang_KhacHang Foreign Key (idkh) References KhachHang(idkh) ON DELETE CASCADE,
Constraint PK_DonHang_QuanLy Foreign Key (idql) References QuanLy(idql) ON DELETE SET NULL
)

/* Tạo bảng Chi Tiết Đơn Hàng*/
Create Table ChiTietDonHang(
iddh int,
idsp int,
sldamua int
Constraint PK_ChiTietDonHang_DonHang Foreign Key (iddh) References DonHang(iddh) ON DELETE CASCADE,
Constraint PK_ChiTietDonHang_SanPham Foreign Key (idsp) References SanPham(idsp) ON DELETE CASCADE
)
/* Thêm dữ liệu vào bảng Quản Lí */
Insert Into QuanLy(sdt, matkhau, hoten, ngaysinh, gioitinh, diachi, email) /* id tự động tăng lên ta bỏ id */
Values ('0984677725', 'truong', N'Trần Văn Trường', '2002/11/19', '0', N'Hà Nam', N'trantruong2002@gmail.com')

Insert Into QuanLy(sdt, matkhau, hoten, ngaysinh, gioitinh, diachi, email) /* id tự động tăng lên ta bỏ id */
Values ('0123456789', 'thanh', N'Nguyễn Công Thành', '2002/09/27', '0', N'Hà Nam', N'thanhnc@gmail.com')

/* Thêm dữ liệu vào bảng khách hàng */
Insert Into KhachHang(sdt, matkhau, hoten,ngaysinh, gioitinh, diachi, email) /* id tự động tăng lên ta bỏ id */
Values ('0984677725', 'truong', N'Trần Văn Trường', '2002/11/19','0', N'Hà Nam', N'trantruong2002@gmail.com'),
	   ('0984573733', 'thien', N'Trần Thanh Thiện', '2002/11/19' ,'0', N'Hà Nam', N'thanhthien2000@gmail.com')

/* Thêm dữ liệu vào bảng Loại sản phẩm */
Insert Into LoaiSP(tenloai)
Values (N'Điện Thoại'),
	   (N'LapTop')

/* Thêm dữ liệu vào bảng Sản phẩm */
Insert Into SanPham(idloai, anhsp, tensp, giasp, thongtinsp, slsanpham) 
Values ('1', 'https://cdn.tgdd.vn/Products/Images/42/290878/Realme-c30s-den-temp-600x600.jpg', N'Realme C30s (4GB/64GB)', '27200000', N'Ra mắt với mức giá bán phải chăng cùng bộ thông số khá ấn tượng, Realme C30s (4GB/64GB) bỗng dưng trở thành chiếc điện thoại Android tâm điểm trong phân khúc giá rẻ. Đây hứa hẹn sẽ là một sự lựa chọn cực kỳ kinh tế mà nhà Realme dành cho người dùng.', '5000'),
	   ('1', 'https://cdn.tgdd.vn/Products/Images/42/301603/realme-c35-den-thumb-600x600.jpg', N'Realme C55 6GB', '4990000', N'Realme C35 chiếc smartphone có vẻ ngoài đơn giản với ngôn ngữ thiết kế vuông vắn và màu sắc thanh lịch, khung viền được làm cứng cáp cùng mặt lưng nhựa nhám giúp mang lại cảm giác cầm chắc tay hay hạn chế việc bám dấu vân tay mỗi khi sử dụng.', '3560'),
	   ('2', 'https://cdn.tgdd.vn/Products/Images/44/264354/TimerThumb/dell-gaming-g15-5511-i5-70266676-(48).jpg', N'Dell Gaming G15 5511 i5 11400H (70266676)', '24190000', N'Không những mang đến cho người dùng hiệu năng ấn tượng nhờ con chip Intel thế hệ 11 tân tiến cùng card rời RTX 30 series, laptop Dell Gaming G15 5511 i5 11400H (70266676) còn sở hữu thiết kế thời thượng, lôi cuốn, hứa hẹn sẽ là người cộng sự lý tưởng cùng bạn chinh phục mọi chiến trường.', '2500'),
	   ('2', 'https://cdn.tgdd.vn/Products/Images/44/228526/asus-rog-zephyrus-ga502iu-r7-al007t-228526-600x600.jpg', N'LapTop Asus ROG Zephyrus G15', '28890000', N'Dell XPS 15 tiếp tục đứng đầu ngai vàng bằng cách thực hiện các chỉnh sửa nhỏ cho đến những thay đổi lớn. Công ty đã cố gắng làm cho laptop nhỏ hơn 5,5% so với mô hình trước đó trong khi làm cho các phím và bàn di chuột lớn hơn đáng kể. Và mặc dù điều đó có vẻ nhỏ, nhưng khung bezel InfinityEdge bốn viền đã biến nó thành một trong những màn hình có độ phân giải cao nhất trên thị trường.
		Và trong khi những thay đổi về thể chất là rất lớn, nội thất của chiếc laptop được xếp hạng hàng đầu này cũng thú vị không kém. Được trang bị bộ vi xử lý thế hệ thứ 10 và GPU Nvidia GeForce. Nó cũng có ổ SSD tốc độ cao và âm thanh tuyệt vời nhờ bộ loa quad mạnh mẽ. Thêm vào đó, nó kéo dài hơn 8 giờ trong bài kiểm tra pin của chúng tôi, điều này tốt cho một laptop 4K.
		Nếu bạn muốn một cỗ máy mạnh mẽ với màn hình tuyệt đẹp, âm thanh xuất sắc và một loạt các tính năng cao cấp khác, Dell XPS 15 là lựa chọn tuyệt vời.', '2500'),
	   ('2', 'https://cdn.tgdd.vn/Products/Images/44/292926/lenovo-thinkpad-x1-carbon-gen-10-i7-21cb00a8vn-2-1.jpg', N'LapTop Lenovo ThinkPad X1 Carbon', '47010000', N'Lenovo đã làm cho laptop doanh nhân yêu thích của chúng tôi trở nên tốt hơn bằng cách cải thiện loa của nó, tạo cho nó một kết cấu sợi carbon mảnh mai và thêm một số tính năng bảo mật hữu ích, bao gồm màn trập webcam và camera IR. Bạn thậm chí còn nhận được micrô trường xa trong trường hợp bạn muốn sử dụng trợ lý kỹ thuật số.
		Trên hết, bạn sẽ có được hiệu suất mạnh mẽ và ổ SSD nhanh. Điều đó có nghĩa là nếu bạn đang làm việc trên các bảng tính lớn với nhiều phép tính, X1 Carbon sẽ xử lý chúng một cách linh hoạt. Bạn cũng có thể mong đợi tuổi thọ pin dài (trên mô hình 1080p) và hai tùy chọn hiển thị tuyệt đẹp, 1080p và 4K, tất cả đều nằm trong một khung máy siêu nhẹ.
		Nhưng chính những tính năng ThinkPad X1 cổ điển đó — khung máy bền bỉ (đã được thử nghiệm trong quân đội), bàn phím tốt nhất trong phân khúc và tính thẩm mỹ màu đen / đỏ thời trang — đã mang X1 Carbon đến sự tuyệt vời.', '1436')

Insert Into SanPham(idloai, tensp, giasp, anhsp, thongtinsp, slsanpham) 
VALUES
(1, N'Điện Thoại iPhone 13 Pro Max 128GB ', 28490000, 'https://cdn.tgdd.vn/Products/Images/42/230529/iphone-13-pro-max-sierra-blue-600x600.jpg', N'iPhone 13 Pro Max 128GB nổi bật với thiết kế cụm 3 camera và cấu hình được nâng cấp. iPhone 13 mang vẻ ngoài sang trọng, tối giản, vuông vắn tương tự như iPhone 12 Pro Max. Màn hình 6.7 inch, kết hợp sử dụng tấm nền OLED, giúp hiển thị hình ảnh rõ ràng, sáng đẹp tự nhiên.\r\nSản phẩm sử dụng chip A15 Bionic cho cấu hình mạnh mẽ, cải thiện tốt khả năng lọc máy. CPU vượt trội đi kèm với RAM 6 GB, có thể đáp ứng tối đa các tác vụ và đặc biệt máy sở hữu bộ nhớ trong 128 GB cho không gian lưu trữ lớn.\r\nNgoài ra, phần camera được nâng cấp lớn nhất từ trước đến nay với con chip xử lý hình ảnh ISP giúp giảm nhiễu, tăng cường độ chi tiết vượt trội. Pin điện thoại có thời lượng sử dụng nhiều hơn iPhone 12 Pro Max 1.5 tiếng, đáp ứng tốt nhu cầu người dùng. Hơn nữa, máy còn được hỗ trợ chuẩn kháng nước và bụi IP68.\r\n', 1000),
(1, N'Điện Thoại iPhone 12 128GB ', 18990000, 'https://cdn.tgdd.vn/Products/Images/42/228736/iphone-12-xanh-la-600x600.jpg', N'iPhone 12 128GB là kết hợp vẻ hoài cổ của iPhone 4 với nét hiện đại của iPhone 11 tạo nên vẻ ngoài hoàn hảo. Toàn bộ thân máy được gói gọn trong khung viền nhôm cao cấp được vát thẳng, ít bo tròn.\r\nMàn hình tràn viền rộng 6.1 inch và áp dụng tấm nền OLED với công nghệ Super Retina XDR cho khả năng hiển thị hình ảnh rõ ràng, sắc nét. Thiết kế mặt kính được trang bị lớp kính cường lực Ceramic Shield, xong áp dụng quá trình trao đổi ion kép trên mặt kính sau, hạn chế máy khỏi bị vết nứt, trầy xước.\r\niPhone 12 được tích hợp chip A14 Bionic cho phép bạn thực hiện 11 nghìn tỷ phép tính mỗi giây, tăng tới 10 lần tốc độ tính toán máy học. Và cụm camera trước TrueDepth 12 MP sở hữu sức mạnh của chip, một loạt các tính năng ưu việt', 2034),
(1, N'Điện Thoại OPPO A95', 6190000, 'https://cdn.tgdd.vn/Products/Images/42/251703/oppo-a95-4g-bac-2-600x600.jpg', N'OPPO A95 mang phong cách thiết kế OPPO Glow mỏng nhẹ, sang trọng, kết hợp với họa tiết nhám được hoàn thiện tỉ mỉ thu hút mọi ánh nhìn. Toàn thân máy đều được làm cong 2.5D mượt mà và có độ mỏng chỉ 7.95 mm.\r\nMàn hình lớn 6.43 inch có độ phân giải Full HD+ cùng công nghệ AMOLED cao cấp, cho không gian hiển hình ảnh chân thực, sống động, để bạn thỏa sức tận hưởng những nội dung yêu thích như xem phim, lướt web, vào mạng xã hội,...\r\nSản phẩm có bộ nhớ trong lên đến 128 GB mang đến không gian lưu trữ lớn, đồng thời nâng cao hiệu suất và tăng tốc độ cài đặt dữ liệu. Và bộ 3 camera 48 MP hỗ trợ công nghệ AI giúp bạn dễ dàng chụp được những bức hình sắc nét và đẹp đến bất ngờ. Đặc biệt, máy sở hữu dung lượng pin lớn, kèm sạc nhanh cho bạn sử dụng thoải mái trong suốt ngày dài.', 980),
(2, N'Máy Tính xách tay HP 348 G7 i3', 22980000, 'https://cdn.tgdd.vn/Products/Images/44/225549/hp-348-g7-i3-1a0z1pa-1-225549-600x600.jpg', N'HP 348 G7 i3 thuộc sản phẩm tầng trung với cấu hình ổn định. Mặc dù sở hữu bộ nhớ RAM 4GB và Intel Core i3 thế hệ 8 nhưng sản phẩm này vẫn đảm bảo các chức năng. Cụ thể như nhập liệu, soạn thảo, học online, làm báo cáo hoặc xem phim trên các trang mạng.\r\nKhi người dùng có nhu cầu thực hiện thêm nhiều tính năng hơn với tốc độ nhanh hơn thì hoàn toàn có thể nâng cấp RAM lên tới 32GB. Bên cạnh đó, việc khởi động máy và tốc độ phản hồi tác vụ còn rất nhanh với SSD 256GB.\"\r\n\"Nếu bạn có nhu cầu phải di chuyển thường xuyên thì HP 348 G7 i3 cũng hoàn toàn phù hợp. Bởi vì trọng lượng của nó chỉ khoảng 1,5kg với màn hình 14 inch gọn nhẹ. Màn hình của thiết bị là sự kết hợp giữa công nghệ Anti-Glare với tấm nền IPS. Từ đó giúp màu sắc hình ảnh rõ nét và tươi sáng hơn.', 2540),
(1, N'Điện Thoại Xiaomi 11T 5G 128GB ', 9990000, 'https://cdn.tgdd.vn/Products/Images/42/249945/oppo-a16k-thumb1-600x600-1-600x600.jpg', N'Xiaomi 11T 5G 128GB đã được trang bị hàng loạt tính năng đỉnh cao như tần số quét màn hình 120 Hz, vi xử lý Dimensity 1200,... chiếc smartphone này nhất định sẽ mang đến sự hài lòng dành cho bạn. Màn hình AMOLED cao cấp 6.67 inch có độ sáng tối đa 1000 nits cho góc nhìn rộng rãi, đi kèm chất lượng hình ảnh hiển thị tốt.\r\nHệ thống 3 camera sau, kết hợp với phần cứng hiện đại được tích hợp bên trong, cho khả năng chụp ảnh cực kì ấn tượng và sẽ không khiến bạn thất vọng. Cấu hình có chip Dimensity 1200 8 nhân có tốc độ xử lý nhanh chóng, tiết kiệm điện năng tiêu thụ hiệu quả.\r\nSản phẩm được trang bị viên pin 5000 mAh đáp ứng tốt nhu cầu sử dụng của dùng trong thời gian dài, đặc biệt máy còn tích hợp thêm công nghệ sạc pin nhanh 67 W tiện lợi\r\n', 1088),
(2, N'Máy Tính Macbook Pro 13 inch (Apple M1)', 35980000, 'https://tuvanmuasam.com/wp-content/uploads/2020/09/Macbook-Pro-13-inch-Apple-M1-300x169.jpg', N'Apple Macbook Pro M1 sở hữu thiết kế sang trọng từ kim loại nguyên khối được kế thừa từ các thế hệ trước nhưng bên trong là một cấu hình cực kỳ đáng gờm. Với chip M1 lần đầu tiên xuất hiện trên MacBook Pro, hiệu năng CPU của máy tăng đến 2.8 lần, hiệu năng GPU tăng 5 lần.\r\nVẫn là thiết kế kim loại nguyên khối sang trọng thường thấy ở các thế hệ trước, phiên bản MacBook Pro lần này mỏng nhẹ chỉ 15.6 mm, trọng lượng 1.4 kg có thể tự tin đồng hành cùng bạn đến bất cứ đâu.\r\nApple Macbook Pro M1 mang trong mình thiết kế độc đáo, di động hiệu năng mạnh mẽ, xử lí nhanh gọn mọi tác vụ, đây chắc chắn là chiếc máy tính xách tay sang trọng và đẳng cấp đáng sở hữu.\r\n', 3492),
(2, N'Máy Tính xách tay Dell XPS 13', 46990000, 'https://cdn.tgdd.vn/Products/Images/44/292594/dell-xps-13-9320-i5-70295789-1.jpg', N'Với ưu điểm dễ thấy nhất là có thiết kế mỏng, nhẹ và cấu hình ổn thì chiếc Dell XPS 13 quả là sự lựa chọn không tồi. Sản phẩm này vô cùng phù hợp với những người thường xuyên phải di chuyển nhiều như doanh nhân, nhân viên sale, bất động sản…\r\nKích thước màn hình của sản phẩm chỉ 13.3 inch, thời lượng pin khỏe, hiệu năng tốt. Với thiết kế này, Dell đã thay đổi vị trí đặt webcam tại trên màn hình thay vì đặt tại bàn phím ở thế hệ trước đó. Ngoài ra, hình ảnh vô cùng sắc nét, âm thanh sống động sẽ giúp bạn có thêm nhiều trải nghiệm thú vị.\"\r\n\"Ở phiên bản model 4K thì thương hiệu này đã nâng cấp lên Core i7, SSD 256GB, RAM 8GB. Điều này cho phép người dùng có thể thực hiện nhiều chức năng cùng lúc như truy cập web, thiết kế hoặc chơi game… khi sử dụng. Đồng thời đảm bảo bộ nhớ trong lưu trữ được nhiều thông tin hơn.\r\n', 4500),
(1, N'Điện Thoại OPPO A16K ', 3090000, 'https://cdn.tgdd.vn/Products/Images/42/249945/oppo-a16k-thumb1-600x600-1-600x600.jpg', N'OPPO A16K thiết kế cong 3D, khung viền được tạo dáng từ nhựa bền nhẹ và độ dày chỉ 7.85 mm tạo nên 1 chiếc điện thoại thông minh thanh mảnh. Màn hình 6.52 inch sử dụng công nghệ màn hình IPS LCD tạo nên khung hình rộng với màu sắc đa dạng và hình ảnh hiển thị tươi tắn, sống động.\r\nSản phẩm sử dụng chip đồ họa xử lý MediaTek Helio G35, giúp cải thiện hình ảnh khi chơi game, cũng như giảm tình trạng giật lag. Bộ nhớ trong 32 GB khá khiêm tốn nhưng bù lại sản phẩm hỗ trợ thẻ dùng MicroSD có thể mở rộng dung lượng lên đến 1 TB.\r\n OPPO A16K có thiết kế camera sau đơn giản, bao gồm 1 máy ảnh độ phân giải 13 MP và 1 đèn flash LED hỗ trợ bạn chụp ảnh tốt hơn trong điều kiện thiếu sáng. Dung lượng pin Li-Po lớn 4230 mAh cho bạn thời gian sử dụng trong cả ngày dài.\r\n', 1023),
(1, N'Điện Thoại Vivo V23e ', 7490000, 'https://cdn.tgdd.vn/Products/Images/42/245607/Vivo-V23e-1-2-600x600.jpg\r\n', N'Vivo V23e lấy cảm hứng từ những phiên bản tiền nhiệm thuộc Vivo V Series để mang đến một thiết kế hoàn hảo hơn, vuông vắn cùng độ mỏng thân máy được tối ưu hơn. Màn hình AMOLED sở hữu kích thước 6.44 inch cho không gian hiển thị lớn và khả năng tái tạo hình ảnh chân thực.\r\nĐiện thoại chạy bằng con chip gaming MediaTek Helio G96 có thể chơi tốt những tựa game như Liên Quân Mobile, PUBG,... Và với bộ nhớ trong 128 GB có không gian lưu trữ lớn, giúp bạn lưu trữ được nhiều hình ảnh hay những bộ phim yêu thích.\r\nĐặc biệt ở chiếc điện thoại này là được trang bị nhiều tính năng chụp ảnh bao gồm: Tự động lấy nét, chụp ảnh xóa phông, chế độ chụp đêm,... Kết hợp với thời lượng pin sử dụng lâu dài khoảng từ 6 tiếng.\r\n', 5600),
(2, N'Máy Tính Dell Inspiron 7306 2-in-1 2021', 24350000, 'https://cdn.tgdd.vn/Products/Images/44/284308/dell…on-16-5620-i5-n6i5003w1-060722-063545-600x600.jpg', 'Dell Inspiron 7306 2-in-1 là một trong những laptop 2-in-1 tốt nhất mà bạn có thể mua với mức giá không quá cao. Điều đó không có gì đáng ngạc nhiên. CPU Tiger Lake thế hệ thứ 11 của Intel với Đồ họa Iris Xe mang lại hiệu suất ổn định và tăng cường đồ họa so với các chip trước đó. Đây là một sự lựa chọn tuyệt vời cho những ai đang tìm kiếm sự linh hoạt cùng với hiệu suất và độ bền và quan trọng là mức giá rất tốt.\r\n Dell Inspiron 7306 được trang bị cấu hình mạnh mẽ so với các sản phẩm cùng phân khúc. Cụ thể, laptop với con chip Intel Core i5-1135G7 cùng con chip Intel Iris Xe Graphics, ổ cứng thể rắn M.2 PCIe NVMe 512GB cùng dung lượng RAM 8GB.Tất cả mang lại cho Inspiron 7306 một hiệu năng mượt mà, tốc độ truyền tải cao cùng khả năng đa nhiệm tốt.\r\n', 1200),
(1, N'Xiaomi Redmi 10 (4GB/64GB) ', 3990000, 'https://cdn.tgdd.vn/Products/Images/42/249080/redmi-10-blue-600x600.jpg', N'Xiaomi Redmi 10 (4GB/64GB) là chiếc điện thoại thông minh lấy lòng người dùng bởi thiết kế sang trọng, độc đáo, sở hữu cụm camera sau có độ phân giải cao và việc sử dụng đến 4 camera, Redmi 10 cho phép bạn có thể chụp những bức ảnh từ khung hình cực rộng đến lấy nét ở cự ly siêu gần.\r\nMàn hình có kích thước lớn 6.5 inch và độ phân giải Full HD+ mang lại không gian lớn để bạn thỏa sức trải nghiệm với chất lượng hình ảnh hiển thị sắc nét. Đặc biệt, cụm loa kép stereo phát ra âm thanh lớn, to rõ và sống động.\r\nChiếc điện thoại này có hiệu năng làm việc, giải trí ổn định nhờ sở hữu sức mạnh của bộ vi xử lý Helio G88 từ MediaTek, đây là phiên bản cải tiến của Helio G85. Ngoài ra, máy còn đi kèm bộ nhớ trong 64 GB tạo không gian lưu trữ lớn.\r\n', 1000),
(1, N'Điện Thoại iPhone XR 64GB ', 17890000, 'https://cdn.tgdd.vn/Products/Images/42/190325/iphone-xr-hopmoi-den-600x600-2-600x600.jpg', N'iPhone XR 64GB sở hữu màn hình rộng OLED cao cấp 6.1 inch với khả năng hiển thị hình ảnh có độ sắc nét cao và vô cùng ấn tượng. Điện thoại trang bị chip mới của Apple là A12 Bionic có ưu điểm xử lý nhanh chóng và mượt mà mọi tác vụ kể các các tựa game có đồ họa khủng hiện nay.\r\nSản phẩm chỉ sở hữu 1 camera sau nhưng có độ phân giải 12 MP, cùng công nghệ chụp ảnh Smart HDR sẽ không làm bạn thất vọng với khả năng chụp ảnh sống ảo của máy. Hệ thống âm thanh chất lượng sẽ mang đến cho bạn trải nghiệm sống động và chân thật nhất.\r\nCuối cùng là hệ thống bảo mật an toàn với Face ID bạn chỉ cần nhìn vào máy, hệ thống sẽ tự động quét khuôn mặt 3D, giúp cho việc mở khóa đơn giản và nhanh chóng hơn.\r\n', 1500),
(1, N'Điện Thoại Realme 7 Pro ', 13450000, 'https://cdn.tgdd.vn/Products/Images/42/227689/realme-7-pro-bac-600x600.jpg', N'Realme 7 Pro lấy cảm hứng từ thiết kế không gian gương trong tự nhiên, kết hợp với sự chia cắt táo bạo mang lại vẻ đẹp độc đáo, sang trọng. Màn hình tràn viền 6.4 inch được trang bị tấm nền Super AMOLED mang lại chất lượng hình ảnh hiển thị sắc nét và góc nhìn rộng hơn.\r\nCụm 4 camera được trang bị nổi bật ở mặt lưng, hỗ trợ các tính năng chụp nâng cao như camera xóa phông, Panorama, HDR,... hiệu quả. Hiệu suất làm việc nhanh chóng đến từ con chip Snapdragon 720G, đi cùng dung lượng RAM 8 GB giúp mọi thao tác trên thiết bị diễn ra ổn định, mượt mà.\r\nBên cạnh đó, điện thoại còn được trang bị pin khủng với dung lượng 4500 mAh cho bạn trải nghiệm làm việc, giải trí thoải mái trong thời gian dài.', 2202),
(1, N'Điện Thoại iPhone 12 Pro Max 128GB ', 27790000, 'https://cdn.tgdd.vn/Products/Images/42/213033/iphone-12-pro-max-xanh-duong-new-600x600-600x600.jpg', N'iPhone 12 Pro Max 128GB ấn tượng với thiết kế phong cách đầy lịch lãm và toát ra vẻ sang trọng ngay từ những ánh nhìn đầu tiên. Khung viền làm từ thép không gỉ cao cấp, mặt kính và mặt lưng đều được phủ lớp kính cường lực Ceramic Shield, hạn chế xước sát trước những va đập thông thường hiệu quả.\r\nKích thước màn hình 6.7 inch, sử dụng tấm nền của OLED có độ sáng tối đa 1200 nits (HDR) giúp hiển thị hình ảnh sắc nét, chân thật nhất. Điện thoại trang bị chip A14 đem đến tốc độ xử lý nhanh hơn 50% so với những vi xử lý khác trên thị trường.\r\nThêm nữa, chiếc iPhone được trang bị hệ thống 3 camera có cùng một độ phân giải là 12 MP, cũng như sở hữu một số tính năng chụp ảnh nổi bật như Smart HDR 3, cảm biến LiDAR,... hỗ trợ bạn chinh phục mọi không gian sống ảo.\r\n', 1066),
(1, N'Điện Thoại Samsung Galaxy Z Fold 5G 256GB', 31990000, 'https://cdn.tgdd.vn/Products/Images/42/226935/samsung-galaxy-z-fold-3-silver-1-600x600.jpg', N'Có thể thấy mẫu smartphone Galaxy Z Fold3 lần này vẫn giữ nguyên ngoại hình cùng cơ chế màn hình gập mở dạng quyển sách như của tiền nhiệm, biến chiếc smartphone thành một chiếc máy tính bảng mini một cách dễ dàng và ngược lại.\r\nVới cấu tạo chắc chắn của khung viền hợp kim nhôm sẽ giúp bạn yên tâm tận hưởng các hoạt động yêu thích một cách trọn vẹn nhất.\r\nNgoài ra, Galaxy Z Fold3 5G cũng là thiết bị màn hình gập đầu tiên trên thế giới sở hữu công nghệ kháng nước chuẩn IPX8 ở mức cao nhất trong thang đo từ 1 - 8 giúp chúng ta yên tâm sử dụng hằng ngày.\r\nVới cảm biến vân tay ở cạnh bên, việc mở khóa màn hình trên Z Fold3 5G giờ đây đã được thực hiện một cách nhanh chóng và an toàn chỉ trong một nốt nhạc.\r\n', 2300),
(1, N'Điện Thoại Samsung Galaxy S22 Ultra 5G 128GB ', 25990000, 'https://cdn.tgdd.vn/Products/Images/42/235838/Galaxy-S22-Ultra-Burgundy-600x600.jpg', N'Samsung Galaxy S22 Ultra 5G 128GB sở hữu thiết kế sang trọng với mặt lưng nhám chống trượt. Đặc biệt, máy được tích hợp bút S - Pen vô cùng tiện lợi.\r\nĐiện thoại sở hữu màn hình lớn 6.8 inch đạt độ phân giải 2K +, cùng với đó là tần số quét 120 Hz cho ra những khung hình sắc nét, thao tác mượt mà nhất.\r\nMáy ảnh của Samsung Galaxy S22 Ultra có khả năng chụp đêm vô cùng ấn tượng. Khả năng zoom camera tới 100x cùng hỗ trợ quay phim 8K là những điểm nổi bật khác.\r\n', 1),
(1, N'Điện thoại iPhone 14 Pro Max ', 33990000, 'https://cdn.tgdd.vn/Products/Images/42/251192/iphone-14-pro-max-tim-thumb-600x600.jpg', N'Điện thoại iPhone 14 Pro Max 128GB là chiếc smartphone hỗ trợ 5G sở hữu màn hình OLED rộng 6.7 inch. Độ sang trọng được thể hiện qua chất liệu thép không gỉ sáng bóng.\r\nMặt trên điện thoại được phủ Ceramic Shield, cho khả năng chống va đập tốt hơn bình thường. Tính năng True Tone và độ sáng 1200 nits giúp máy hiển thị rõ ngay cả ngoài trời nắng.\r\nCamera được trang bị loạt công nghệ như zoom kỹ thuật số, quay video HDR, chụp ảnh ProRAW hay tính năng Deep Fusion.\r\n', 1),
(1, N'Điện Thoại Samsung Galaxy S22+ 5G 128GB ', 19990000, 'https://cdn.tgdd.vn/Products/Images/42/242439/Galaxy-S22-Plus-White-600x600.jpg', N'Samsung Galaxy S22+ 5G 128GB là mẫu điện thoại có khung viền được chế tạo từ hợp kim nhôm, đi kèm kính cường lực Gorilla Glass Victus+.\r\nMáy sở hữu thiết kế màn hình tràn viền rộng 6.6 inch, đạt độ phân giải Full HD+. Tần số quét 120 Hz đảm bảo sự mượt mà cho mọi tác vụ.\r\nĐặc biệt, điện thoại được trang bị chip Snapdragon 8 Gen 1, có khả năng xử lý các tác vụ game nặng dễ dàng, đi kèm các chế độ chụp ảnh ưu việt.', 1),
(1, N'Điện Thoại Xiaomi 12', 15990000, 'https://cdn.tgdd.vn/Products/Images/42/234621/Xiaomi-12-xam-thumb-mau-600x600.jpg', N'Xiaomi 12 là mẫu flagship có thiết kế gọn gàng, tinh tế đi kèm hiệu năng mạnh mẽ và khả năng chụp ảnh siêu việt.\r\nMáy sở hữu màn hình AMOLED tràn viền kích thước 6.28 inch, kết hợp cùng công nghệ True Color, cũng như đạt độ bao phủ màu DCI-P3.\r\nHiệu năng khủng của điện thoại đến từ chính con chip Snapdragon 8 Gen 1, cùng với đó là 8 GB RAM và 256 GB bộ nhớ trong.\r\n', 1);

Select * From SanPham

/* Thêm dữ liệu vào bảng giỏ hảng */
Insert Into GioHang (idkh, idsp, slchon)
Values ('1', '1', '3'),
	   ('2', '1', '3'),
	   ('1', '2', '1')

/* Thêm dữ liệu vào bảng Đơn Hàng */
Insert Into DonHang(idkh, idql, hoten, diachi, sdt, email, ngaylap)
Values ('1', '1', N'Nguyễn Thị Trà Mi', N'Hà Nam', '098582822', 'trami123@gmail.com', GETDATE())

/* Thêm dữ liệu vào bảng Chi Tiết Đơn Hàng */
Insert Into ChiTietDonHang(iddh, idsp, sldamua)
Values ('1', '1', '2')

Select * From DonHang
select * from ChiTietDonHang


/* -------------------------------------- Hàm LOGIN -------------------------------------*/
/* Khởi tạo hàm thủ tục cho Đăng Nhập khách hàng */
Go
Create Proc sp_login (@sdt varchar(20), @matkhau varchar(50))
As
Begin
	Select * From KhachHang Where sdt = @sdt And matkhau = @matkhau;
End

Exec sp_login '0984677725', 'truong'


/* Khởi tạo hàm thủ tục cho Đăng Nhập admin */
Go
create Proc sp_login_admin (@sdt varchar(20), @matkhau varchar(50))
As
Begin
	Select * From QuanLy Where sdt = @sdt And matkhau = @matkhau;
End

Exec sp_login_admin '0376018919', 'admin'

/* Khởi tạo hàm thủ tục cho Đăng Kí Admin */
Go
Create Proc sp_register_admin (@sdt varchar(20), @matkhau varchar(50), @hoten nvarchar(100), @ngaysinh date, @gioitinh int, @diachi nvarchar(50), @email nvarchar(100))
As
Begin
	Insert into QuanLy(sdt, matkhau, hoten, ngaysinh, gioitinh, diachi, email)
	values (@sdt,@matkhau, @hoten, @ngaysinh,@gioitinh, @diachi, @email)
End



/*------------------------------------- Admin -----------------------------------------*/

/*------------------------------- Sản Phẩm ------------------------------------------------*/

/* Khởi tạo hàm thủ tục thêm 1 sản phẩm mới */
Go
Create Proc sp_add_product (@idloai int, @anhsp varchar(100), @tensp nvarchar(100), @giasp int, @thongtinsp nvarchar(1000), @slsanpham int)
As
Begin
	Insert Into SanPham (idloai, anhsp, tensp, giasp, thongtinsp, slsanpham) 
	Values (@idloai, @anhsp, @tensp, @giasp, @thongtinsp, @slsanpham)
End

Exec sp_add_product '2', 'https://cdn.tgdd.vn/Products/Images/44/201243/asus-zenbook-13-ux333fa-a4016t-1-600x600.jpg ', N'Máy Tính Asus ZenBook 13 UX325EA', '2990000', N'Asus ZenBook 13 UX325EA nằm giữa ranh giới doanh nhân và cao cấp. Bạn nhận được một hệ thống làm rung chuyển vẻ ngoài đẹp đẽ của các laptop hàng đầu của Asus với một số tính năng bảo mật và độ bền của ExpertBook, dòng kinh doanh mới của Asus. Thêm vào đó, bạn nhận được thêm sức mạnh của chip Thế hệ thứ 11 mới của Intel và tất cả các tính năng mà sáng kiến &ZeroWidthSpace;&ZeroWidthSpace;Evo của Intel mang lại, bao gồm cả thời lượng pin hơn 13 giờ. Và bạn thậm chí có thể chơi game (chỉ cần không ở cài đặt cao nhất).
Laptop này đã vượt qua các đối thủ cạnh tranh một cách dễ dàng trong các bài kiểm tra điểm chuẩn khắt khe của chúng tôi, khiến nó trở thành một lựa chọn tuyệt vời cho công việc và giải trí. Thêm vào đó, đây là một trong số ít laptop có webcam tích hợp tốt nhờ mô-đun máy ảnh mới và thuật toán máy ảnh của Asus.',
'2360'

/* Khởi tạo hàm thủ tục cập nhật 1 sản phẩm */
Go
Create Proc sp_update_product (@idsp int, @idloai int, @anhsp varchar(100), @tensp nvarchar(100), @giasp int, @thongtinsp nvarchar(1000), @slsanpham int)
As
Begin
	Update SanPham 
	Set idloai = @idloai, anhsp = @anhsp, tensp = @tensp, giasp = @giasp, thongtinsp = @thongtinsp, slsanpham = @slsanpham
	Where idsp = @idsp
End

Exec sp_update_product '5', '1', '.', N'HIHI' , '10000', N'KK', '111'

/* Khởi tạo hàm thủ tục xóa 1 sản phẩm */
Go
Create Proc sp_delete_product (@idsp int)
As
Begin
	Delete From SanPham Where idsp = @idsp
End

Exec sp_delete_product '5'

/*----------------------------------- Quản Lí ------------------------------------*/
/* Khởi tạo hàm thủ tục lấy ra thông tin quản lí theo idql */
Go
Create Proc ql_qlchitiet(@idql int)
As
Begin
	Select * From QuanLy Where idql = @idql
End

/* Lấy danh sách người quản lý */
Go
Create Proc sp_manager_all
As
Begin
	Select * from QuanLy
End

Exec sp_manager_all

/* cập nhật người quản lý */
Go
Create Proc ql_update (@idql int, @sdt varchar(20), @matkhau varchar(50), @hoten nvarchar(100), @ngaysinh date, @gioitinh int, @diachi nvarchar(50), @email nvarchar(100))
As
Begin
	Update QuanLy 
	Set sdt = @sdt, matkhau = @matkhau, hoten = @hoten, ngaysinh = @ngaysinh, gioitinh = @gioitinh, diachi = @diachi, email = @email
	Where idql = @idql
End

/* Xóa người quản lý */
Go
create Proc sp_delete_manager(@idql int)
As
Begin
	delete QuanLy where idql = @idql
End

Exec sp_delete_manager 1


/* ---------------------------------- Loại Sản Phẩm ----------------------------------*/
/* Khởi tạo hàm thủ tục lấy ra loại sản phẩm */
Go
Create Proc type_typechitiet(@idloai int)
As
Begin
	Select * From LoaiSp Where idloai = @idloai
End

/* --------------------------------Khách hàng------------------------------- */

/* GetAll */
Go
Create Proc sp_client_all
As
Begin
	Select * from KhachHang
End

Exec sp_client_all


/* Delete khách hàng */
Go
Create Proc sp_delete_client(@idkh int)
As
Begin
	delete KhachHang where idkh = @idkh
End

Exec sp_delete_client 2

select *from KhachHang
select *from SanPham
select *from DonHang
select *from QuanLy

delete SanPham where idsp=1





/*------------------------------------- Khách Hàng -----------------------------------------*/

/*----------------------------------- Sản Phẩm ------------------------------------*/
/* Khởi tạo hàm thủ tục lấy ra 15 sản phẩm mới nhất */

Go
Create Proc sp_product_all
As
Begin
	Select * From SanPham 
End

Exec sp_product_all

Go
Create Proc sp_product_new 
As
Begin
	Select Top(15) * From SanPham Order By idsp Desc
End

Exec sp_product_new

/* Khởi tạo hàm thủ tục lấy ra 5 sản phẩm laptop mới nhất */
Go
Create Proc sp_laptop_new 
As
Begin
	Select Top(5) * From SanPham Where idloai = 2 Order By idsp Desc
End

Exec sp_laptop_new

/* Khởi tạo hàm thủ tục lấy ra toàn bộ điện thoại */
Go
Create Proc sp_smartphone
As
Begin
	Select * From SanPham Where idloai = 1
End

Exec sp_smartphone

/* Khởi tạo hàm thủ tục lấy ra toàn bộ điện thoại */
Go
Create Proc sp_laptop
As
Begin
	Select * From SanPham Where idloai = 2
End

Exec sp_laptop

/* Khởi tạo hàm thủ tục chuyển sang form chi tiết */
Go
Create Proc sp_spchitiet(@idsp int)
As
Begin
	Select * From SanPham Where idsp = @idsp
End

Exec sp_spchitiet '1';

/*------------------------------------ Giỏ Hàng ----------------------------------------*/
/* Khởi tạo hàm thủ tục thêm dữ liệu vào giỏ hàng */
Go
Create Proc post_cart(@idkh int, @idsp int, @slchon int)
As
Begin
	Insert Into GioHang (idkh, idsp, slchon) 
	Values (@idkh, @idsp, @slchon)
End

Go
Create Proc ud_gh ( @idkh int, @idsp int, @slchon int)
As
Begin
	Update GioHang 
	Set idsp = @idsp, slchon = @slchon
	Where idkh = @idkh
End


Go
Create Proc gh_chitiet(@idkh int)
As
Begin
	Select * From GioHang Where idkh = @idkh
End

Go
Create Proc gh_delete (@idkh int)
As
Begin
	Delete From GioHang Where idkh = @idkh
End



Go
Create Proc sp_client_all
As
Begin
	Select * from KhachHang
End

Exec sp_client_all


/* Delete khách hàng */
Go
Create Proc sp_delete_client(@idkh int)
As
Begin
	delete KhachHang where idkh = @idkh
End

Exec sp_delete_client 2


Go
Create Proc sp_order_all
As
Begin
	Select * from DonHang
End

Exec sp_order_all


Go
Create proc sp_productType_all
As
Begin
	Select * from LoaiSP
End
exec sp_productType_all

Go
Create Proc sp_type (@tenloai varchar(20))
As
Begin
	Insert into LoaiSP(tenloai)
	values (@tenloai)
End

Go
Create Proc type_update (@idloai int,@tenloai varchar(20))
As
Begin
	Update LoaiSP 
	Set tenloai = @tenloai
	Where idloai = @idloai
End

Go
create Proc type_delete(@idloai int)
As
Begin
	delete LoaiSP where idloai = @idloai
End

/* Khởi tạo hàm thủ tục hiển thị sản phẩm có trong giỏ hàng */
Go
Create Proc sp_cart (@idkh int)
As
Begin
	Select idgh, idkh, SanPham.idsp, idloai, anhsp, tensp, giasp, thongtinsp, slsanpham, slchon
	From GioHang, SanPham
	Where GioHang.idsp = SanPham.idsp
	AND idkh = @idkh
End

Exec sp_cart '1'

/* Khởi tạo hàm thủ tục tăng số lượng sản phẩm chọn */
Go
Create Proc sp_increase_cart (@idgh int)
As
Begin
	declare @sl int
	select @sl = slchon from GioHang where idgh = @idgh
	if @sl >= 10
		Update GioHang Set slchon = 10 Where idgh = @idgh
	else
		Update GioHang Set slchon += 1 Where idgh = @idgh
End

Exec sp_increase_cart '5'

/* Khởi tạo hàm thủ tục giảm số lượng sản phẩm chọn */
Go
Create Proc sp_reduce_cart (@idgh int)
As
Begin
	declare @sl int
	select @sl = slchon from GioHang where idgh = @idgh
	if @sl <= 1
		Update GioHang Set slchon = 1 Where idgh = @idgh
	else
		Update GioHang Set slchon -= 1 Where idgh = @idgh
End

Exec sp_reduce_cart '6'

/* Khởi tạo hàm thủ tục xóa sản phẩm trong giỏ hàng */
Go
Create Proc sp_delete_cart (@idgh int)
As
Begin
	Delete From GioHang Where idgh = @idgh
End

Exec sp_delete_cart '2'

Select * from GioHang

/*-------------------------------------- Khách Hàng -------------------------------------------*/

/* Khởi tạo hàm thủ tục lấy dữ liệu khách hàng thông qua idkh */
Go
Create Proc sp_data_client (@idkh int)
As
Begin
	Select * From KhachHang Where idkh = @idkh
End

Exec sp_data_client '1'
/*------------------------------------- Hóa Đơn ----------------------------------------*/

/*Lịch sử mua hàng */
Go
Create Proc sp_hoadon(@idkh int)
As
Begin
	Select d.iddh, idkh, idql, c.idsp, idloai, hoten, diachi, sdt, sldamua, anhsp, tensp, giasp, thongtinsp, slsanpham, ngaylap
	From SanPham s, DonHang d, ChiTietDonHang c
	Where s.idsp = c.idsp And d.iddh = c.iddh And idkh = @idkh
End
exec sp_hoadon '1'

/* Khởi tạo hàm thủ tục insert 1 dữ liệu hóa đơn mới */
Go
Create Proc sp_add_order 
(
	@idkh int,
	@idql int,
	@hoten nvarchar(100),
	@diachi nvarchar(50),
	@sdt varchar(20),
	@email nvarchar(100)
)
As
Begin
	Insert Into DonHang(idkh, idql, hoten, diachi, sdt, email, ngaylap)
	Values (@idkh, @idql, @hoten, @diachi, @sdt, @email, GETDATE())
End

Exec sp_add_order '2', '1', N'Hồ Quang Tuấn', N'Hà Nam', '09856282838', 'quangtuan123@gmail.com'

/* Khởi tạo hàm thủ tục lấy ra id hóa đơn mới nhất */
Go
	Create Proc sp_iddh 
As
Begin
	Select Top (1) iddh From DonHang Order By iddh Desc
End

Exec sp_iddh
/*---------------------------- Chi Tiết Hóa Đơn ------------------------------------------------*/

/* Khởi tạo hàm thủ tục insert 1 dữ liệu chi tiết hóa đơn mới */
Go
Create Proc sp_add_order_details
(
	@iddh int,
	@idsp int,
	@sldamua int
)
As
Begin
	Insert Into ChiTietDonHang(iddh, idsp, sldamua)
	Values (@iddh, @idsp, @sldamua)
End

Exec sp_add_order_details '2', '2', '3'

/*----------------------------------- Khách Hàng ----------------------------*/
/* Khởi tạo hàm thủ tục cho Đăng Nhập */
Go
Create Proc sp_login_client (@sdt varchar(20), @matkhau varchar(50))
As
Begin
	Select * From KhachHang Where sdt = @sdt And matkhau = @matkhau;
End

Exec sp_login_client '0984677725', 'truong'

/* Khởi tạo hàm thủ tục cho Đăng Ký */
Go
Create Proc sp_register_client (@sdt varchar(20), @matkhau varchar(50), @hoten nvarchar(100), @ngaysinh date, @gioitinh tinyint, @diachi nvarchar(50), @email nvarchar(100))
As
Begin
	Insert into KhachHang(sdt, matkhau, hoten, ngaysinh, gioitinh, diachi, email)
	values (@sdt,@matkhau, @hoten, @ngaysinh,@gioitinh, @diachi, @email)
End

Exec sp_register_client '0128856789', 'tung123', N'Trịnh Đức Tùng', '2002-01-25', 0, N'Vĩnh Phúc', 'tungtt@gmail.com'

Go
Create Proc sp_checkEmail ( @email nvarchar(100))
As
Begin
	select count(*) from KhachHang where email = @email
End

/* Hàm thủ tục lấy sản phẩm theo id */
go
create proc sp_getClient_id(@idkh int)
As
begin
	select * from KhachHang where idkh = @idkh
end
exec sp_getClient_id 1

/* Khởi tạo hàm thủ tục cập nhật */
Go
Create Proc sp_update_client (@idkh int, @sdt varchar(20), @matkhau varchar(50), @hoten nvarchar(100), @ngaysinh date, @gioitinh tinyint, @diachi nvarchar(50), @email nvarchar(100))
As
Begin
	Update KhachHang 
	Set sdt = @sdt, matkhau = @matkhau, hoten = @hoten, ngaysinh = @ngaysinh, gioitinh = @gioitinh, diachi = @diachi, email = @email
	Where idkh = @idkh
End


Select * From SanPham Where idsp = 33
Select * From ChiTietDonHang
Select * From GioHang
Delete From DonHang Where iddh = 18
Select * From QuanLy


