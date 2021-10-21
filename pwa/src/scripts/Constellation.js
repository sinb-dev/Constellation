
import Database from '../scripts/Database.js'

export default {
    checkSetup() {
        Database.loadConfiguration()

    }
}