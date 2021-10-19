
import Database from '../scripts/Database.js'

export default {
    checkSetup() {
        Database.checkSetupComplete().then(() => console.log("Ready")).catch(function() {
            //document.location.href="/setup"
        })
    }
}