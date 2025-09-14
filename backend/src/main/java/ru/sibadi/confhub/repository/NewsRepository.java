package ru.sibadi.confhub.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.sibadi.confhub.entity.News;

import java.util.UUID;

public interface NewsRepository extends JpaRepository<News, UUID> {

}
