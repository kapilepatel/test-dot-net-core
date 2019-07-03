pipeline {
     agent any

    stages {
        stage('Checkout') {
            steps {
                    git credentialsId: 'userId', url: 'https://github.com/kapilepatel/test-dot-net-core', branch: 'master'
            }
        }
        stage('Restore PACKAGES') {
            steps {
                bat "dotnet restore"
            }
        }
        
        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o out'
            }
        }
        stage('Build') {
            steps {
                bat 'dotnet out/AG_MS_Authentication.dll'
            }
        }
    }



}