

---------------

create table Admin(
	MaAdmin char(10) not null primary key,
	UserAd varchar(50),
	Email varchar(50),
	Pass varchar(50),
)

create table SanPham(
	MaSP char(10) not null primary key,
	TenSP nvarchar(max),
	SoLuong int,
	DonGia int, 
	MoTa nvarchar(max),
	GiaKM int,
	URLAnh varchar(max),
	MaLoaiSP char(10),
	MaKM char(10),
)

create table PhieuXuat(
	MaPX char(10) not null primary key,
	NgayDat date,
	NgayShip date,
	MaND char(10),
)

create table CTPhieuXuat(
	MaPX char(10) not null ,
	MaSP char(10) not null ,
	SoLuong int,
	primary key(MaPX,MaSP),
)

create table PhieuNhap(
	MaPN char(10) not null primary key,
	NgayNhap date,
	MaNCC char(10),
)

create table CTPhieuNhap(
	MaPN char(10) not null ,
	MaSP char(10) not null ,
	SoLuong int,
	DonGia int,
	primary key(MaPN,MaSP),
)
create table GioHang(
	MaGH char(10) not null primary key,
	MaND char(10),
)

create table CTGioHang(
	MaGH char(10) not null ,
	MaSP char(10) not null ,
	SoLuong int,
	primary key(MaGH,MaSP),
)
create table NguoiDung(
	MaND char(10) not null primary key,
	SDT varchar(50),
	GioiTinh char(10),
	Tuoi int,
	DiaChi nvarchar(max),
	UserName varchar(50),
	Email varchar(50),
	Pass varchar(50),
	MaLoaiND char(10),
	TenND nvarchar(max),
)

create table LoaiND(
	MaLoaiND char(10) not null primary key,
	TenLoai nvarchar(max),
	SoDinhDanh int,
	MaKM char(10),
)
create table BangKhuyenMai(
	MaKM char(10) not null primary key,
	TienKM int,
	TiLeKM int,
)
create table BinhLuan(
	MaBL char(10) not null primary key,
	NoiDung nvarchar(max),
	NgayDang date,
	LuotThich int,
	MaSP char(10),
	MaND char(10),
)
create table LoaiSP(
	MaLoaiSP char(10) not null primary key,
	TenLoai nvarchar(max),
	MoTa nvarchar(max),
	URLAnh varchar(max),
)
create table NhaCC(
	MaNCC char(10)not null primary key,
	Ten nvarchar(max),
	DiaChi nvarchar(max),
	SDT varchar(50),
	Email varchar(max),
)

alter table dbo.PhieuNhap add foreign key(MaNCC) references dbo.NhaCC(MaNCC)
alter table dbo.CTPhieuNhap add foreign key(MaPN) references dbo.PhieuNhap(MaPN)
alter table dbo.CTPhieuNhap add foreign key(MaSP) references dbo.SanPham(MaSP)
alter table dbo.CTPhieuXuat add foreign key(MaPX) references dbo.PhieuXuat(MaPX)
alter table dbo.CTPhieuXuat add foreign key(MaSP) references dbo.SanPham(MaSP)
alter table dbo.CTGioHang add foreign key(MaGH) references dbo.GioHang(MaGH)
alter table dbo.CTGioHang add foreign key(MaSP) references dbo.SanPham(MaSP)
alter table dbo.PhieuXuat add foreign key(MaND) references dbo.NguoiDung(MaND)
alter table dbo.NguoiDung add foreign key(MaLoaiND) references dbo.LoaiND(MaLoaiND)
alter table dbo.SanPham add foreign key(MaLoaiSP) references dbo.LoaiSP(MaLoaiSP)
alter table dbo.SanPham add foreign key(MaKM) references dbo.BangKhuyenMai(MaKM)
alter table dbo.LoaiND add foreign key(MaKM) references dbo.BangKhuyenMai(MaKM)
alter table dbo.BinhLuan add foreign key(MaSP) references dbo.SanPham(MaSP)
alter table dbo.BinhLuan add foreign key(MaND) references dbo.NguoiDung(MaND)
alter table dbo.SanPham add foreign key(MaNCC) references dbo.NhaCC(MaNCC)