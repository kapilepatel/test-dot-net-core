pipeline {
     agent any

    stages {
        stage('Checkout') {
            steps {
                    git url: 'https://github.com/kapilepatel/test-dot-net-core', branch: 'master'
            }
        }
        stage('Restore PACKAGES') {
            steps {                   
                    dir ("test-dot-net-core"){
                        sh(script:"dotnet restore")                   
                    }
            }              
        }

        stage('Testing') {
            steps {                   
                    dir ("test-project"){
                        sh(script:"dotnet test")                   
                    }
            }              
        }
        
        stage('Publish') {
            steps {
                dir("test-dot-net-core"){                
                    sh(script:"dotnet publish -c Release -o out")
                }
            }
            
        }
        stage('Build') {
            steps {
                dir("test-dot-net-core"){                
                    //sh(script:"dotnet out/AG_MS_Authentication.dll")
                    sh(script:"nohup dotnet AG_MS_Authentication.dll &")
                }
            }
        }
    }



}