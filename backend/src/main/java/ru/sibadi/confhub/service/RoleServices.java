package ru.sibadi.confhub.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import ru.sibadi.confhub.entity.Roles;
import ru.sibadi.confhub.repository.PeopleRepository;
import ru.sibadi.confhub.repository.RolesRepository;

import java.util.Optional;
import java.util.Set;
import java.util.UUID;

@Service
public class RoleServices {

    @Autowired
    private RolesRepository rolesRepository;

    @Autowired
    private PeopleRepository peopleRepository;

    public Roles createRole(Roles role){
        Roles savedRole = rolesRepository.save(role);
        return savedRole;
    }

    public Set<Roles> getRolesByTitles(Set<String> rolesTitles){
        Optional<Set<Roles>> optRoles = rolesRepository.findByTitleIn(rolesTitles);
        return optRoles.get();
    }

    public Roles getRoleById(String id){
        Optional<Roles> optRole = rolesRepository.findById(UUID.fromString(id));
        return optRole.get();
    }

}
