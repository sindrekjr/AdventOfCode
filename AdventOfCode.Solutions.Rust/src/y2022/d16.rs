use std::{
    cell::RefCell,
    cmp,
    collections::{HashMap, HashSet},
};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day16::solve_part_one(input),
        Part::P2 => Day16::solve_part_two(input),
    }
}

#[derive(Debug)]
struct Valve {
    name: String,
    flow: usize,
    adjacent: HashSet<String>,
}

impl From<&str> for Valve {
    fn from(scan: &str) -> Self {
        let (data, connections) = scan.split_once(';').unwrap();
        let (valve, flow_str) = data.split_once(" has flow rate=").unwrap();
        let name = valve.split_whitespace().last().unwrap().to_owned();
        let flow = flow_str.parse::<usize>().unwrap();
        let adjacent = connections
            .trim()
            .split_whitespace()
            .skip(4)
            .map(|t| t.trim_end_matches(',').to_owned())
            .collect();

        Self {
            name,
            flow,
            adjacent,
        }
    }
}

struct Day16;
impl Solution for Day16 {
    fn solve_part_one(input: String) -> String {
        let valves: HashMap<String, Valve> = input
            .lines()
            .map(|scan| {
                let valve = Valve::from(scan);
                (valve.name.to_string(), valve)
            })
            .collect();

        let mut distances = HashMap::new();

        for x in valves.keys() {
            for y in valves.keys() {
                if valves.get(x).unwrap().adjacent.contains(y) {
                    distances
                        .entry((x.clone(), y.clone()))
                        .or_insert(RefCell::new(1));
                } else {
                    distances
                        .entry((x.clone(), y.clone()))
                        .or_insert(RefCell::new(i64::MAX));
                }
            }
        }

        for x in valves.keys() {
            for y in valves.keys() {
                for z in valves.keys() {
                    let y_to_z = get(&distances, y, z);
                    let y_to_x = get(&distances, y, x);
                    let x_to_z = get(&distances, x, z);

                    distances.insert(
                        (y.clone(), z.clone()),
                        RefCell::new(match y_to_x == i64::MAX || x_to_z == i64::MAX {
                            true => cmp::min(y_to_z, i64::MAX),
                            false => cmp::min(y_to_z, y_to_x + x_to_z),
                        }),
                    );
                }
            }
        }

        String::new()
    }

    fn solve_part_two(_input: String) -> String {
        String::new()
    }
}

fn get(distances: &HashMap<(String, String), RefCell<i64>>, x: &String, y: &String) -> i64 {
    *distances.get(&(x.clone(), y.clone())).unwrap().borrow()
}
