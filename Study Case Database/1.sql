SELECT
  `kelas`.`name` AS `Kelas`,
  `guru`.`name` AS `Nama Guru`,
  `siswa`.`name` AS `Nama Siswa`
FROM
  `test`.`classes` AS `kelas`
  INNER JOIN `test`.`teachers` AS `guru`
    ON (
      `kelas`.`teacher_id` = `guru`.`id`
    )
  INNER JOIN `test`.`students` AS `siswa`
    ON (
      `kelas`.`id` = `siswa`.`class_id`
    );