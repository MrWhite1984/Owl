package ru.sibadi.confhub.exceptions;

public class EmailAlreadyExistsException extends RuntimeException {
    public EmailAlreadyExistsException(String email){
        super("A user with " + email + " email already exists in the database.");
    }
}
