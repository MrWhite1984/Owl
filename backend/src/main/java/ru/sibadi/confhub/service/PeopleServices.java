package ru.sibadi.confhub.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import ru.sibadi.confhub.entity.People;
import ru.sibadi.confhub.repository.PeopleRepository;

import java.util.Optional;
import java.util.UUID;

@Service
public class PeopleServices {

    @Autowired
    private PeopleRepository peopleRepository;

    @Autowired
    private PasswordEncoder passwordEncoder;

    public People createPeople(People people){
        people.setPassword(passwordEncoder.encode(people.getPassword()));
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
