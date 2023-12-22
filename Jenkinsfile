node ('slave01'){
    try {

        checkout scm
        def image="car-service"
        def DockerComoseName = "docker-compose.dev.yml"
        def registry = "hunghvhpu/${image}"
        def Workspace = "/usr/src/workspace/${env.JOB_NAME}"
        def ComposePath  = '/usr/local/bin/docker-compose'
        def SourcePath = "${Workspace}"

        stage('Build') {
            checkout scm
            docker.build(registry + ":$BUILD_NUMBER" , "-f l2404/Dockerfile  .")
        }
        
        stage('Deploy to DevelopEnv') {
            withEnv(["VERSION=$BUILD_NUMBER"]) {
                    sh "cd ${SourcePath} && ${ComposePath} -f ${DockerComoseName} down"
                    sh "cd ${SourcePath} && ${ComposePath} -f ${DockerComoseName} -p ${image} up -d"
            }
        }

    }catch (e) {
        throw e
    } finally {
        
    }
}
