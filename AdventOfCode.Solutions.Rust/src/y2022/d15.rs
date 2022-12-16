use super::coor::Position;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day15::solve_part_one(input),
        Part::P2 => Day15::solve_part_two(input),
    }
}

struct Sensor {
    position: Position<isize>,
    closest_beacon: Position<isize>,
}

impl Sensor {
    fn from(report: &str) -> Self {
        let (sensor_str, beacon_str) = report.split_once(':').unwrap();

        Self {
            position: Position::from_object_str(sensor_str),
            closest_beacon: Position::from_object_str(beacon_str),
        }
    }

    fn beacon_distance(&self) -> isize {
        self.position.manhattan(&self.closest_beacon)
    }

    fn within_coverage(&self, position: &Position<isize>) -> bool {
        self.position.manhattan(position) <= self.beacon_distance()
    }

    fn perimeter(&self) -> Vec<Position<isize>> {
        let distance = self.beacon_distance() + 1;
        let y = self.position.y;
        let mut offset = 0;

        (self.position.x - distance..self.position.x + distance).flat_map(move |x| {
            let positions = vec![Position { x, y: y + offset }, Position { x, y: y - offset }];

            if x < self.position.x {
                offset += 1;
            } else {
                offset -= 1;
            }

            positions
        }).collect()
    }
}

impl Position<isize> {
    fn from_object_str(obj: &str) -> Self {
        let (_, coor) = obj.split_once("at ").unwrap();
        let (x, y) = coor.split_once(',').unwrap();

        Self {
            x: x.split_once('=').unwrap().1.parse::<isize>().unwrap(),
            y: y.split_once('=').unwrap().1.parse::<isize>().unwrap(),
        }
    }

    fn manhattan(&self, other: &Position<isize>) -> isize {
        (self.x - other.x).abs() + (self.y - other.y).abs()
    }
}

struct Day15;
impl Solution for Day15 {
    fn solve_part_one(input: String) -> String {
        // const Y: isize = 10;
        const Y: isize = 2_000_000;
        let mut x_min = isize::MAX;
        let mut x_max = isize::MIN;

        let sensors: Vec<Sensor> = input
            .lines()
            .map(|line| {
                let sensor = Sensor::from(line);
                let beacon_distance = sensor.beacon_distance();

                if sensor.position.x - beacon_distance < x_min {
                    x_min = sensor.position.x - beacon_distance;
                }

                if sensor.position.x + beacon_distance > x_max {
                    x_max = sensor.position.x + beacon_distance;
                }

                sensor
            })
            .collect();

        (x_min..x_max)
            .fold(0, |acc, x| {
                let pos = &Position { x, y: Y };

                if sensors
                    .iter()
                    .any(|sensor| sensor.within_coverage(pos) && pos != &sensor.closest_beacon)
                {
                    acc + 1
                } else {
                    acc
                }
            })
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        const MIN: isize = 0;
        const MAX: isize = 4_000_000;
        // const MAX: isize = 20;

        let sensors: Vec<Sensor> = input.lines().map(|line| Sensor::from(line)).collect();

        let beacon = sensors
            .iter()
            .find_map(|sensor| {
                sensor
                    .perimeter()
                    .iter()
                    .filter(|pos| pos.x >= MIN && pos.y >= MIN && pos.x <= MAX && pos.y <= MAX)
                    .find_map(|pos| {
                        if sensors.iter().any(|sensor| sensor.within_coverage(&pos)) {
                            None
                        } else {
                            Some(pos.clone())
                        }
                    })
            })
            .unwrap();

        (beacon.x * 4_000_000 + beacon.y).to_string()
    }
}
