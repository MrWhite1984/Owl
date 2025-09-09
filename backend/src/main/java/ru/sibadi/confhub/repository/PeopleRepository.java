package ru.sibadi.confhub.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.sibadi.confhub.entity.People;

import java.util.Optional;
import java.util.UUID;

public interface PeopleRepository extends JpaRepository<People, UUID> {
    Optional<People> findByEmail(String email);
}
