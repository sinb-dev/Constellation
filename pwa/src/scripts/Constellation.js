
import Database from '../scripts/Database.js'

export default {
    checkSetup() {
        Database.loadConfiguration()
        .then(() => {
            console.log("Ready")
            Database.sync();
        })
        .catch(function(e) {
            console.error(e);
            //document.location.href="/setup"
        })
    }
}