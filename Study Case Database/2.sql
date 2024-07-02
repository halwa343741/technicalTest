SELECT guru.name AS `Nama Guru`, GROUP_CONCAT(kelas.name SEPARATOR ', ') AS Kelas
FROM classes kelas
JOIN teachers guru ON kelas.teacher_id = guru.id
GROUP BY guru.name