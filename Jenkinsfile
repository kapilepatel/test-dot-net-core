pipeline {
     agent any

    stages {
        stage('Checkout') {
            steps {
                    git url: 'https://github.com/kapilepatel/test-dot-net-core', branch: 'master'
            }
        }
        stage('Restore PACKAGES') {
             
            sh(script:"dotnet restore")
            
        }
        
        stage('Publish') {
            
                sh(script:"dotnet publish -c Release -o out")
            
        }
        stage('Build') {
            steps {
                sh(script:"dotnet out/AG_MS_Authentication.dll")
            }
        }
    }



}