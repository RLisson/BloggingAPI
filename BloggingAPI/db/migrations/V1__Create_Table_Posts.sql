CREATE TABLE `posts` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`title` VARCHAR(50) NOT NULL COLLATE 'latin1_swedish_ci',
	`content` VARCHAR(100) NOT NULL COLLATE 'latin1_swedish_ci',
	`category` VARCHAR(20) NOT NULL COLLATE 'latin1_swedish_ci',
	`created_at` DATETIME(6) NOT NULL,
	`updated_at` DATETIME(6) NOT NULL,
	PRIMARY KEY (`id`) USING BTREE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
;