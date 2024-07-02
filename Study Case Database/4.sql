DELIMITER $$

CREATE
    PROCEDURE `test`.`siswa_kelas_guru_sp`()
	BEGIN
        SELECT 
            students.name AS `Nama Siswa`,
            classes.name AS Kelas,
            teachers.name AS `Nama Guru`
        FROM 
            students
        JOIN 
            classes ON students.class_id = classes.id
        JOIN 
            teachers ON classes.teacher_id = teachers.id;
	END$$

DELIMITER ;