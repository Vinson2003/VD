INSERT INTO [dbo].[mtRole] ([role]) VALUES ('Admin')

INSERT INTO [dbo].[mtAdmin]
           ([created_by]
           ,[created]
           ,[updated_by]
           ,[updated]
           ,[username]
           ,[email]
           ,[password]
           ,[password_salt]
           ,[status]
           ,[role_id])
     VALUES
           (null
           ,null
           ,null
           ,null
           ,'admin'
           ,''
           ,'402e5ac685b9df973cf5e49b763e306951b13284f3e285eea793382e8734bf53'
           ,'-E0RXyzGVULTQ9r:g^bx:/Pp7I2konerw(45(k*0o/P/VP)1~%c.7WC:^T@o'
           ,'ENABLED'
           ,'1')
