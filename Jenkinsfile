pipeline {
    agent any
    environment {
        email = credentials('aseward-email-address')
    }
    stages {
        stage('Deploy') {
            when {
                anyOf {
                    branch 'master'
                }
            }
            stages {
                stage('docfx to gh-pages') {
                    steps {
                        withCredentials([usernamePassword(credentialsId: 'ajseward-github-with-password', usernameVariable: 'GIT_USER', passwordVariable: 'GIT_PASS')]) {
                            script {
                                docker.image('erothejoker/docker-docfx:latest').inside() {
                                    sh """
                                    docfx DocFX/docfx.json
                                    git config --global user.name "$GIT_USER"
                                    git config --global user.email "${email}"
                                    git config --local credential.helper "!f() { echo username=\\$GIT_USER; echo password=\\$GIT_PASS; }; f"
                                    git add .
                                    git commit -m "Build DocFX"
                                    git subtree split --prefix=DocFX/pages -b gh-pages
                                    git push -f origin gh-pages
                                    """
                                }
                            }
                        }
                    }
                }
                stage('build upm') {
                    steps {
                        script {
                            sh """
                            git subtree split --prefix=Assets/GameJamStarterKit -b upm
                            git push -f origin upm
                            """
                        }
                    }
                }
            }
        }
    }
}