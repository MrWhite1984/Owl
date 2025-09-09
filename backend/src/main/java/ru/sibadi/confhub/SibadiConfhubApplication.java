package ru.sibadi.confhub;

import org.slf4j.LoggerFactory;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import org.slf4j.Logger;

@SpringBootApplication
public class SibadiConfhubApplication {

	private static final Logger logger = LoggerFactory.getLogger(SibadiConfhubApplication.class);

	public static void main(String[] args) {

		SpringApplication.run(SibadiConfhubApplication.class, args);
		logger.info("System is started");
	}

}
