Pigeon Mailer
=============

About
-----

Pigeon Mailer is a helper library to send emails with templates, you can create templates with a set of keys and then tell Pigeon to parse it.
Its sits on top of the SmtpClient class of the .Net framework so you need to [config the mailSettings in the system.net directive][L1] of your config file.
For tests you can use the [SpecifiedPickupDirectory][L2] method on the deliveryMethod attribute of the smtp node in mailSettings.

[L1]: http://msdn.microsoft.com/en-us/library/w355a94k.aspx
[L2]: http://www.gotripod.com/2009/08/03/asp-net-smtp-setting-a-pickup-directory-for-development/

The EmailTemplate Class
-----------------------

This is the core class of the library, here is how to use some properties and methods

### The Body and BodyFilePath properties ###
If you set the property BodyFilePath with a absolute path to a file it sets the Body property with the contents of the file.
If you wish you can set the Body property with a string.

### The ToAddress property ###
Set the recipient of the message. You can set as many you want

### How Pigeon treats your templates ###
There are two ways to define values for your templates:

###### The Parameters way ######
Add key,values pair to the Parameters property.
Let's say we have this template:
	Hello {name},
	Here are some information about your profile:
		*Age: {age}
		*Gender: {gender}
		*Country: {country}
Then to add values:
	var template = new EmailTemplate();
	//read the body
	tamplate.BodyFilePath = @"c:\temp\my_template.txt" //
	//set some properties
	template.Subject = "Hello {name}" //yes, you can have key in the subject
	//add the values
	template.Parameters.Add("{name}", "Toshio");
	template.Parameters.Add("{age}", 27);
	template.Parameters.Add("{gender}", "male");
	template.Parameters.Add("{country}", "Brasil");
	//tell Pigeon do its job
	template.ProcessTemplate();

###### The Object way ######
Create a instance of any object which contains the values to populate your template.
Let's say we have this template:
	Hello {name},
	Here are some information about your profile:
		*Age: {age}
		*Gender: {gender}
		*Country: {country}
Then to add values:
	var template = new EmailTemplate();
	//read the body
	tamplate.BodyFilePath = @"c:\temp\my_template.txt" //
	//set some properties
	template.Subject = "Hello {name}" //yes, you can have key in the subject
	//create some object or use anyone you have
	var someObj = new {
		name = "Toshio"
		,age = 27
		,gender = "male"
		,country = "Brasil"
	}
	//tell Pigeon do its job
	template.ProcessTemplate(someObj);
	
How to send a message
---------------------

Ater create a instance of EmailTemplate you need to instantiate the Sender class and use it to send your message.
	Sender sender = new Sender();
	sender.SendMessage(template);
And you are done.