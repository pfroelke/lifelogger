export default class Utils {
    static addHoursToDate(date: Date, hours: number): Date {
        const toReturn = new Date(date);
        toReturn.setHours(date.getHours() + hours);
        return toReturn;
    }

    static getMinutesString(date: Date): string {
        return (date.getMinutes() < 10 ? '0' : '') + date.getMinutes();
    }

    static dateToString(date: Date): string {
        const minutes = (date.getMinutes() < 10 ? '0' : '') + date.getMinutes();
        const hours = (date.getHours() < 10 ? '0' : '') + date.getHours();
        const day = (date.getDate() < 10 ? '0' : '') + date.getDate();
        const month = (date.getMonth() < 10 ? '0' : '') + date.getMonth();
        const year = date.getFullYear();
        return `${hours}:${minutes} ${day}.${month}.${year}`;
    }

    static stringToTime(str: string): number {
        const n = str.match(/\d+/g).map(Number);
        console.log(n);
        const date = new Date(n[4], n[3], n[2], n[0], n[1]);
        return date.getTime();
    }

    static timeToHours(time: number): number {
        return Math.round(time / (1000 * 60 * 60));
    }
}
