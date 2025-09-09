package ru.sibadi.confhub.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.sibadi.confhub.entity.Roles;

import java.util.Optional;
import java.util.Set;
import java.util.UUID;

public interface RolesRepository extends JpaRepository<Roles, UUID> {
    Optional<Set<Roles>> findByTitleIn(Set<String> titles);
}
