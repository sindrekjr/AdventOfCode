use std::collections::HashSet;

use crate::{
    core::{Part, Solution},
    utils::grid::Coordinate,
};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day14::solve_part_one(input),
        Part::P2 => Day14::solve_part_two(input),
    }
}

const W: u8 = 101;
const H: u8 = 103;

struct Robot {
    position: Coordinate,
    velocity: Coordinate,
}

impl From<&str> for Robot {
    fn from(robo_str: &str) -> Self {
        let [position, velocity]: [Coordinate; 2] = robo_str
            .split(' ')
            .map(|part| {
                let (_, coor_str) = part.split_once('=').unwrap();
                let (x, y) = coor_str.split_once(',').unwrap();
                Coordinate {
                    x: x.parse().unwrap(),
                    y: y.parse().unwrap(),
                }
            })
            .collect::<Vec<Coordinate>>()
            .try_into()
            .unwrap();

        Self { position, velocity }
    }
}

impl Robot {
    fn prediction(&self, seconds: isize) -> Coordinate {
        let x = (self.position.x + self.velocity.x * seconds).rem_euclid(W as isize);
        let y = (self.position.y + self.velocity.y * seconds).rem_euclid(H as isize);

        Coordinate { x, y }
    }
}

struct Day14;
impl Solution for Day14 {
    fn solve_part_one(input: String) -> Option<String> {
        Some(
            input
                .lines()
                .fold([0, 0, 0, 0], |mut q, robo_str| {
                    let prediction = Robot::from(robo_str).prediction(100);

                    match (
                        prediction.x < (W / 2) as isize,
                        prediction.y < (H / 2) as isize,
                        prediction.x > (W / 2) as isize,
                        prediction.y > (H / 2) as isize,
                    ) {
                        (true, true, false, false) => q[0] += 1,
                        (false, true, true, false) => q[1] += 1,
                        (true, false, false, true) => q[2] += 1,
                        (false, false, true, true) => q[3] += 1,
                        _ => (),
                    };

                    q
                })
                .into_iter()
                .product::<u32>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let robots: Vec<Robot> = input
            .lines()
            .map(|robo_str| Robot::from(robo_str))
            .collect();
        let robo_count = robots.len();

        Some(
            (1..100_000_000)
                .find(|seconds| {
                    robots
                        .iter()
                        .map(|robot| robot.prediction(*seconds))
                        .collect::<HashSet<Coordinate>>()
                        .len()
                        == robo_count
                })
                .unwrap()
                .to_string(),
        )
    }
}
