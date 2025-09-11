package ru.sibadi.confhub.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import ru.sibadi.confhub.entity.People;
import ru.sibadi.confhub.repository.PeopleRepository;

import java.util.HashSet;
import java.util.Optional;
import java.util.Set;
import java.util.UUID;

@Service
public class PeopleServices {

    @Autowired
    private PeopleRepository peopleRepository;

    @Autowired
    private PasswordEncoder passwordEncoder;

    @Autowired
    private RoleServices roleServices;

    public People createPeople(People people){
        people.setPassword(passwordEncoder.encode(people.getPassword()));
        if(peopleRepository.count() == 0) {
            Set<String> roles = Set.of("ADMIN", "MODER", "USER");
            people.setRoles(roleServices.getRolesByTitles((roles)));
        }
        People savePeople = peopleRepository.save(people);
        return savePeople;
    }

    public People getPeopleById(UUID id){
        return peopleRepository.getById(id);
    }

    public People authenticate(String email, String rawPassword){
        Optional<People> optPeople = peopleRepository.findByEmail(email);
        if(optPeople.isPresent()){
            People user = optPeople.get();
            if(passwordEncoder.matches(rawPassword, user.getPassword()))
                return user;
        }
        return null;
    }
}
