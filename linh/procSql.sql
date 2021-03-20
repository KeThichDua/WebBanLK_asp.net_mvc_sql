CREATE PROC ThongKe_Nhap
AS
BEGIN
	SELECT pn.MaPN, pn.MaNCC, ncc.Ten AS TenNCC, SUM(SoLuong) AS  TotalQuantities, SUM(DonGia) AS TotalPrices
	FROM PhieuNhap pn
	INNER JOIN CTPhieuNhap ct ON ct.MaPN = pn.MaPN
	INNER JOIN dbo.NhaCC ncc ON ncc.MaNCC = pn.MaNCC
	GROUP BY pn.MaPN, pn.MaNCC, ncc.Ten
END
GO
CREATE PROC ThongKe_Nhap_Khoang_Thoi_gian @startDate DATETIME, @endDate DATETIME
AS
BEGIN
	SELECT pn.MaPN, pn.MaNCC, ncc.Ten AS TenNCC, SUM(SoLuong) AS  TotalQuantities, SUM(DonGia) AS TotalPrices
	FROM PhieuNhap pn
	INNER JOIN CTPhieuNhap ct ON ct.MaPN = pn.MaPN
	INNER JOIN dbo.NhaCC ncc ON ncc.MaNCC = pn.MaNCC
	WHERE pn.NgayNhap BETWEEN @startDate AND @endDate
	GROUP BY pn.MaPN, pn.MaNCC, ncc.Ten
END
GO
CREATE PROC ThongKe_Xuat
AS
BEGIN
	SELECT px.MaPX, nd.TenND , px.NgayDat, px.NgayShip, SUM(ct.SoLuong) AS  TotalQuantities, SUM(ct.SoLuong*sp.DonGia) AS TotalPrices
	FROM dbo.PhieuXuat AS px
	INNER JOIN dbo.CTPhieuXuat ct ON ct.MaPX = px.MaPX
	INNER JOIN dbo.SanPham AS sp ON sp.MaSP = ct.MaSP
	INNER JOIN dbo.NguoiDung AS nd ON nd.MaND = px.MaND
	GROUP BY px.MaPX, nd.TenND , px.NgayDat, px.NgayShip
END
GO
CREATE PROC ThongKe_Xuat_Khoang_Thoi_gian @startDate DATETIME, @endDate DATETIME
AS
BEGIN
	SELECT px.MaPX, nd.TenND , px.NgayDat, px.NgayShip, SUM(ct.SoLuong) AS  TotalQuantities, SUM(ct.SoLuong*sp.DonGia) AS TotalPrices
	FROM dbo.PhieuXuat AS px
	INNER JOIN dbo.CTPhieuXuat ct ON ct.MaPX = px.MaPX
	INNER JOIN dbo.SanPham AS sp ON sp.MaSP = ct.MaSP
	INNER JOIN dbo.NguoiDung AS nd ON nd.MaND = px.MaND
	WHERE px.NgayDat BETWEEN @startDate AND @endDate
	GROUP BY px.MaPX, nd.TenND , px.NgayDat, px.NgayShip
END
GO
CREATE PROC ThongKe_Top_10SP_BanChay
AS
BEGIN
	SELECT TOP(10) sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TotalQuantities, SUM(ct.SoLuong*sp.DonGia) AS TotalPrices
	FROM dbo.SanPham AS sp
	INNER JOIN dbo.CTPhieuXuat AS ct ON ct.MaSP = sp.MaSP
	GROUP BY sp.MaSP, sp.TenSP
END
GO
CREATE PROC ThongKe_Top_10SP_BanChay_Trong_Khoang_Thoi_gian @startDate DATETIME, @endDate DATETIME
AS
BEGIN
	SELECT TOP(10) sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TotalQuantities, SUM(ct.SoLuong*sp.DonGia) AS TotalPrices
	FROM dbo.SanPham AS sp
	INNER JOIN dbo.CTPhieuXuat AS ct ON ct.MaSP = sp.MaSP
	INNER JOIN dbo.PhieuXuat AS px ON px.MaPX = ct.MaPX
	WHERE px.NgayDat BETWEEN @startDate AND @endDate
	GROUP BY sp.MaSP, sp.TenSP
END
GO
CREATE PROC ThongKe_Xuat_Theo_Nguoi_Dung
AS
BEGIN
	SELECT nd.TenND, nd.SDT, nd.DiaChi, SUM(ct.SoLuong) AS  TotalQuantities, SUM(ct.SoLuong*sp.DonGia) AS TotalPrices
	FROM dbo.PhieuXuat AS px
	INNER JOIN dbo.CTPhieuXuat ct ON ct.MaPX = px.MaPX
	INNER JOIN dbo.SanPham AS sp ON sp.MaSP = ct.MaSP
	INNER JOIN dbo.NguoiDung AS nd ON nd.MaND = px.MaND
	GROUP BY nd.TenND, nd.SDT, nd.DiaChi
END
GO
CREATE PROC ThongKe_Xuat_Theo_Nguoi_Dung_Khoang_Thoi_gian @startDate DATETIME, @endDate DATETIME
AS
BEGIN
	SELECT nd.TenND, nd.SDT, nd.DiaChi, SUM(ct.SoLuong) AS  TotalQuantities, SUM(ct.SoLuong*sp.DonGia) AS TotalPrices
	FROM dbo.PhieuXuat AS px
	INNER JOIN dbo.CTPhieuXuat ct ON ct.MaPX = px.MaPX
	INNER JOIN dbo.SanPham AS sp ON sp.MaSP = ct.MaSP
	INNER JOIN dbo.NguoiDung AS nd ON nd.MaND = px.MaND
	WHERE px.NgayDat BETWEEN @startDate AND @endDate
	GROUP BY nd.TenND, nd.SDT, nd.DiaChi
END
GO