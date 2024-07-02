CREATE VIEW siswa_kelas_guru_view AS
SELECT 
    siswa.name AS `Nama Siswa`,
    kelas.name AS Kelas,
    guru.name AS `Nama Guru`
FROM 
    students AS siswa
JOIN 
    classes AS kelas ON siswa.class_id = kelas.id
JOIN 
    teachers AS guru ON kelas.teacher_id = guru.id;