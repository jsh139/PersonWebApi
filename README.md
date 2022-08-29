# PersonWebApi

This application is intended to be used as a tool to interview potential QA engineers.  Here we are focused on looking at candidates with API testing experience using Postman, Fiddler, etc.  The app has been deployed to a Heroku container.

## Exercise Instructions

There is a Web Api that returns a list of people.  
The base Url for the Web Api is located at:  https://personapi-jsh139.herokuapp.com/
As of now, there are only two endpoints defined.  
- “/Person” which is an HTTP GET.  It returns a list of people.
- “/Person/{id}” which is an HTTP GET.  It returns a single person.

Authentication is done via a request header called “X-ApiKey”.  There are two values that may be used.  One has full access and returns the full set of data, another has limited access and returns data with sensitive info redacted.

The Api Keys are:
- Full Access - 3ce68e99-c33d-4dda-b6f5-e48377e24f55
- Limited Access - b920171c-6aeb-4ea8-881b-2b076035e4a2

Test the /Person endpoints in this Web Api using Postman or your choice of HTTP request composer

