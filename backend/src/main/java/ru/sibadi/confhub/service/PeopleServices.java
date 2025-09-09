package ru.sibadi.confhub.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import ru.sibadi.confhub.entity.People;
import ru.sibadi.confhub.repository.PeopleRepository;

@Service
public class PeopleServices {

    @Autowired
    private PeopleRepository peopleRepository;

    public People createPeople(People people){
        People savePeople = peopleRepository.save(people);
        return savePeople;
    }

}
