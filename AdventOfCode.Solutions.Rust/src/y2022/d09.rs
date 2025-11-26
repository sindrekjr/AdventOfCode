use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day08::solve_part_one(input),
        Part::P2 => Day08::solve_part_two(input),
    }
}

struct Trail {
    knots: Vec<(i32, i32)>,
    length: usize,
}

impl Trail {
    fn new(length: usize) -> Self {
        Self {
            knots: vec![(0, 0); length],
            length,
        }
    }

    fn do_moves<I>(&mut self, moves: I) -> HashSet<(i32, i32)>
    where
        I: Iterator<Item = (char, usize)>,
    {
        moves
            .flat_map(|(mv, steps)| {
                (0..steps)
                    .map(|_| {
                        match mv {
                            'R' => self.knots[0].0 += 1,
                            'L' => self.knots[0].0 -= 1,
                            'U' => self.knots[0].1 += 1,
                            'D' => self.knots[0].1 -= 1,
                            _ => panic!(),
                        }

                        self.catch_up();
                        self.knots[self.length - 1]
                    })
                    .collect::<Vec<(i32, i32)>>()
            })
            .collect()
    }

    fn catch_up(&mut self) {
        for i in 1..self.length {
            let head = self.knots[i - 1];
            let slack = self.knots[i];

            if !adjacent(&head, &slack) && !same(&head, &slack) {
                self.knots[i] = *self
                    .peek_around(head)
                    .iter()
                    .find(|adj| adjacent(adj, &slack))
                    .unwrap();
            }
        }
    }

    fn peek_around(&self, (x, y): (i32, i32)) -> [(i32, i32); 8] {
        [
            (x, y - 1),
            (x, y + 1),
            (x - 1, y),
            (x + 1, y),
            (x + 1, y + 1),
            (x - 1, y - 1),
            (x + 1, y - 1),
            (x - 1, y + 1),
        ]
    }
}

struct Day08;
impl Solution for Day08 {
    fn solve_part_one(input: String) -> Option<String> {
        Some(
            Trail::new(2)
                .do_moves(input.lines().map(parse_move))
                .len()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        Some(
            Trail::new(10)
                .do_moves(input.lines().map(parse_move))
                .len()
                .to_string(),
        )
    }
}

fn parse_move(mv: &str) -> (char, usize) {
    let (direction, steps) = mv.split_once(' ').unwrap();
    return (
        direction.chars().collect::<Vec<char>>()[0],
        steps.parse().unwrap(),
    );
}

fn adjacent(a: &(i32, i32), b: &(i32, i32)) -> bool {
    (a.0 - b.0).abs() <= 1 && (a.1 - b.1).abs() <= 1
}

fn same(a: &(i32, i32), b: &(i32, i32)) -> bool {
    a.0 == b.0 && a.1 == b.1
}
