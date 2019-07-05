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
                 sh(script:"cd test-dot-net-core")
                 sh(script:"dotnet restore")
              }
        }
        
        stage('Publish') {
             steps {
                sh(script:"dotnet publish -c Release -o out")
             }
            
        }
        stage('Build') {
            steps {
                sh(script:"dotnet out/AG_MS_Authentication.dll")
            }
        }
    }



}