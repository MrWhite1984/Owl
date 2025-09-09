package ru.sibadi.confhub.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.sibadi.confhub.entity.People;

import java.util.UUID;

public interface PeopleRepository extends JpaRepository<People, UUID> {
}
