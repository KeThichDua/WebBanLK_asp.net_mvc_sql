select P.MaPX, S.TenSP, C.SoLuong, P.NgayDat, P.NgayShip
from PhieuXuat P, CTPhieuXuat C, SanPham S
where P.MaPX = C.MaPX AND S.MaSP = C.MaSP