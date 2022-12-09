use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day08::solve_part_one(input),
        Part::P2 => Day08::solve_part_two(input),
    }
}

struct Day08;
impl Solution for Day08 {
    fn solve_part_one(input: String) -> String {
        let mut head = vec![(0, 0)];
        let mut tail = vec![(0, 0)];

        input.lines().for_each(|mv| {
            let (direction, steps) = mv.split_once(' ').unwrap();
            
            (0..steps.parse::<usize>().unwrap()).for_each(|_| {
                let prev = head.last().unwrap().to_owned();
                let next = match direction {
                    "R" => (prev.0 + 1, prev.1),
                    "L" => (prev.0 - 1, prev.1),
                    "U" => (prev.0, prev.1 + 1),
                    "D" => (prev.0, prev.1 - 1),
                    _ => panic!()
                };

                head.push(next);

                if !adjacent(tail.last().unwrap(), &next) {
                    tail.push(prev);
                }
            })
        });

        tail.into_iter().collect::<HashSet<(i32, i32)>>().len().to_string()
    }

    fn solve_part_two(input: String) -> String {
        let mut head = vec![(0, 0); 10];
        let mut tail = vec![(0, 0)];

        input.lines().for_each(|mv| {
            let (direction, steps) = mv.split_once(' ').unwrap();
            
            (0..steps.parse::<usize>().unwrap()).for_each(|_| {
                let prev = head[0];
                let next = match direction {
                    "R" => (prev.0 + 1, prev.1),
                    "L" => (prev.0 - 1, prev.1),
                    "U" => (prev.0, prev.1 + 1),
                    "D" => (prev.0, prev.1 - 1),
                    _ => panic!()
                };

                head.insert(0, next);
                let end = head.pop().unwrap();

                if !adjacent(tail.last().unwrap(), &end) {
                    tail.push(end);
                }
            })
        });

        tail.into_iter().collect::<HashSet<(i32, i32)>>().len().to_string()
    }
}

fn adjacent(a: &(i32, i32), b: &(i32, i32)) -> bool {
    (a.0 - b.0).abs() <= 1 && (a.1 - b.1).abs() <= 1
}
