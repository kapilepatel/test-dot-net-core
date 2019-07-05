pipeline {
     agent any

    stages {
        stage('Checkout') {
            steps {
                    git url: 'https://github.com/kapilepatel/test-dot-net-core', branch: 'master'
            }
        }
        stage('Restore PACKAGES') {
            
                dir("test-dot-net-core"){
                    sh(script:"dotnet restore")
                }
                          
        }
        
        // stage('Publish') {
        //     dir("test-dot-net-core"){
        //         steps {
        //             sh(script:"dotnet publish -c Release -o out")
        //         }
        //     }
            
        // }
        // stage('Build') {
        //     dir("test-dot-net-core"){
        //         steps {
        //             sh(script:"dotnet out/AG_MS_Authentication.dll")
        //         }
        //     }
        // }
    }



}